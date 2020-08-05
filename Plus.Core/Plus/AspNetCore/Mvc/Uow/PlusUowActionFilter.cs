#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Plus.AspNetCore.Uow;
using Plus.DependencyInjection;
using Plus.Uow;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.Uow
{
    public class PlusUowActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PlusUnitOfWorkDefaultOptions _defaultOptions;

        public PlusUowActionFilter(IUnitOfWorkManager unitOfWorkManager, IOptions<PlusUnitOfWorkDefaultOptions> options)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _defaultOptions = options.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();
            var unitOfWorkAttr = UnitOfWorkHelper.GetUnitOfWorkAttributeOrNull(methodInfo);

            context.HttpContext.Items["_PlusActionInfo"] = new PlusActionInfoInHttpContext
            {
                IsObjectResult = context.ActionDescriptor.HasObjectResult()
            };

            if (unitOfWorkAttr?.IsDisabled == true)
            {
                await next();
                return;
            }

            var options = CreateOptions(context, unitOfWorkAttr);

            //Trying to begin a reserved UOW by PlusUnitOfWorkMiddleware
            if (_unitOfWorkManager.TryBeginReserved(PlusUnitOfWorkMiddleware.UnitOfWorkReservationName, options))
            {
                var result = await next();
                if (!Succeed(result))
                {
                    await RollbackAsync(context);
                }

                return;
            }

            //Begin a new, independent unit of work
            using (var uow = _unitOfWorkManager.Begin(options))
            {
                var result = await next();
                if (Succeed(result))
                {
                    await uow.CompleteAsync(context.HttpContext.RequestAborted);
                }
            }
        }

        private PlusUnitOfWorkOptions CreateOptions(ActionExecutingContext context, UnitOfWorkAttribute unitOfWorkAttribute)
        {
            var options = new PlusUnitOfWorkOptions();

            unitOfWorkAttribute?.SetOptions(options);

            if (unitOfWorkAttribute?.IsTransactional == null)
            {
                options.IsTransactional = _defaultOptions.CalculateIsTransactional(
                    autoValue: !string.Equals(context.HttpContext.Request.Method, HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase)
                );
            }

            return options;
        }

        private async Task RollbackAsync(ActionExecutingContext context)
        {
            var currentUow = _unitOfWorkManager.Current;
            if (currentUow != null)
            {
                await currentUow.RollbackAsync(context.HttpContext.RequestAborted);
            }
        }

        private static bool Succeed(ActionExecutedContext result)
        {
            return result.Exception == null || result.ExceptionHandled;
        }
    }
}

#endif