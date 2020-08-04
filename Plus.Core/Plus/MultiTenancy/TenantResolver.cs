using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using System;

namespace Plus.MultiTenancy
{
    public class TenantResolver : ITenantResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PlusTenantResolveOptions _options;

        public TenantResolver(IOptions<PlusTenantResolveOptions> options, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public TenantResolveResult ResolveTenantIdOrName()
        {
            var result = new TenantResolveResult();

            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new TenantResolveContext(serviceScope.ServiceProvider);

                foreach (var tenantResolver in _options.TenantResolvers)
                {
                    tenantResolver.Resolve(context);

                    result.AppliedResolvers.Add(tenantResolver.Name);

                    if (context.HasResolvedTenantOrHost())
                    {
                        result.TenantIdOrName = context.TenantIdOrName;
                        break;
                    }
                }
            }

            return result;
        }
    }
}