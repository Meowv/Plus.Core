using Plus.DependencyInjection;
using Plus.DynamicProxy;
using System;
using System.Reflection;

namespace Plus.Uow
{
    public static class UnitOfWorkInterceptorRegistrar
    {
        public static void RegisterIfNeeded(IOnServiceRegistredContext context)
        {
            if (ShouldIntercept(context.ImplementationType))
            {
                context.Interceptors.TryAdd<UnitOfWorkInterceptor>();
            }
        }

        private static bool ShouldIntercept(Type type)
        {
            return !DynamicProxyIgnoreTypes.Contains(type) && UnitOfWorkHelper.IsUnitOfWorkType(type.GetTypeInfo());
        }
    }
}