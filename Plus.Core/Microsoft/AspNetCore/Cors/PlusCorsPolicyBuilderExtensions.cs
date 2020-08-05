#if NETCOREAPP3_1

using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Microsoft.AspNetCore.Cors
{
    public static class PlusCorsPolicyBuilderExtensions
    {
        public static CorsPolicyBuilder WithPlusExposedHeaders(this CorsPolicyBuilder corsPolicyBuilder)
        {
            return corsPolicyBuilder.WithExposedHeaders("_PlusErrorFormat");
        }
    }
}

#endif