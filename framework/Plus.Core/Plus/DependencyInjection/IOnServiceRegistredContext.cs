using Plus.Collections;
using Plus.DynamicProxy;
using System;

namespace Plus.DependencyInjection
{
    public interface IOnServiceRegistredContext
    {
        ITypeList<IPlusInterceptor> Interceptors { get; }

        Type ImplementationType { get; }
    }
}