#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Plus.Clients;
using Plus.DependencyInjection;
using Plus.MultiTenancy;
using Plus.Tracing;
using Plus.Users;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Serilog
{
    public class PlusSerilogMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ICurrentClient _currentClient;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;
        private readonly ICorrelationIdProvider _correlationIdProvider;
        private readonly PlusAspNetCoreSerilogOptions _options;

        public PlusSerilogMiddleware(
            ICurrentTenant currentTenant,
            ICurrentUser currentUser,
            ICurrentClient currentClient,
            ICorrelationIdProvider correlationIdProvider,
            IOptions<PlusAspNetCoreSerilogOptions> options)
        {
            _currentTenant = currentTenant;
            _currentUser = currentUser;
            _currentClient = currentClient;
            _correlationIdProvider = correlationIdProvider;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var enrichers = new List<ILogEventEnricher>();

            if (_currentTenant?.Id != null)
            {
                enrichers.Add(new PropertyEnricher(_options.EnricherPropertyNames.TenantId, _currentTenant.Id));
            }

            if (_currentUser?.Id != null)
            {
                enrichers.Add(new PropertyEnricher(_options.EnricherPropertyNames.UserId, _currentUser.Id));
            }

            if (_currentClient?.Id != null)
            {
                enrichers.Add(new PropertyEnricher(_options.EnricherPropertyNames.ClientId, _currentClient.Id));
            }

            var correlationId = _correlationIdProvider.Get();
            if (!string.IsNullOrEmpty(correlationId))
            {
                enrichers.Add(new PropertyEnricher(_options.EnricherPropertyNames.CorrelationId, correlationId));
            }

            using (LogContext.Push(enrichers.ToArray()))
            {
                await next(context);
            }
        }
    }
}

#endif