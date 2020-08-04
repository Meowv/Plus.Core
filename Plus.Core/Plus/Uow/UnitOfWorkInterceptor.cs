using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using Plus.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace Plus.Uow
{
    public class UnitOfWorkInterceptor : PlusInterceptor, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PlusUnitOfWorkDefaultOptions _defaultOptions;

        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager, IOptions<PlusUnitOfWorkDefaultOptions> options)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _defaultOptions = options.Value;
        }

        public override async Task InterceptAsync(IPlusMethodInvocation invocation)
        {
            if (!UnitOfWorkHelper.IsUnitOfWorkMethod(invocation.Method, out var unitOfWorkAttribute))
            {
                await invocation.ProceedAsync();
                return;
            }

            using (var uow = _unitOfWorkManager.Begin(CreateOptions(invocation, unitOfWorkAttribute)))
            {
                await invocation.ProceedAsync();
                await uow.CompleteAsync();
            }
        }

        private PlusUnitOfWorkOptions CreateOptions(IPlusMethodInvocation invocation, [CanBeNull] UnitOfWorkAttribute unitOfWorkAttribute)
        {
            var options = new PlusUnitOfWorkOptions();

            unitOfWorkAttribute?.SetOptions(options);

            if (unitOfWorkAttribute?.IsTransactional == null)
            {
                options.IsTransactional = _defaultOptions.CalculateIsTransactional(
                    autoValue: !invocation.Method.Name.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase)
                );
            }

            return options;
        }
    }
}