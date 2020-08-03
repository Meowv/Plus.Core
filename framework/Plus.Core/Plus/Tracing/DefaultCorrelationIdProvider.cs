using Plus.DependencyInjection;
using System;

namespace Plus.Tracing
{
    public class DefaultCorrelationIdProvider : ICorrelationIdProvider, ISingletonDependency
    {
        public string Get()
        {
            return CreateNewCorrelationId();
        }

        protected virtual string CreateNewCorrelationId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}