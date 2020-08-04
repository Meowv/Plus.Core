using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Plus.Data
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        protected PlusDbConnectionOptions Options { get; }

        public DefaultConnectionStringResolver(IOptionsSnapshot<PlusDbConnectionOptions> options)
        {
            Options = options.Value;
        }

        public virtual string Resolve(string connectionStringName = null)
        {
            //Get module specific value if provided
            if (!connectionStringName.IsNullOrEmpty())
            {
                var moduleConnString = Options.ConnectionStrings.GetOrDefault(connectionStringName);
                if (!moduleConnString.IsNullOrEmpty())
                {
                    return moduleConnString;
                }
            }

            //Get default value
            return Options.ConnectionStrings.Default;
        }
    }
}