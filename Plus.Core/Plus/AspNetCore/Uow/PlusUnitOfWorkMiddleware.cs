#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Plus.DependencyInjection;
using Plus.Uow;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Uow
{
    public class PlusUnitOfWorkMiddleware : IMiddleware, ITransientDependency
    {
        public const string UnitOfWorkReservationName = "_PlusActionUnitOfWork";

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PlusUnitOfWorkMiddleware(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (var uow = _unitOfWorkManager.Reserve(UnitOfWorkReservationName))
            {
                await next(context);
                await uow.CompleteAsync(context.RequestAborted);
            }
        }
    }
}

#endif