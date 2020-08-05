#if NETCOREAPP3_1

using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Routing
{
    public class PlusEndpointRouterOptions
    {
        public List<Action<EndpointRouteBuilderContext>> EndpointConfigureActions { get; }

        public PlusEndpointRouterOptions()
        {
            EndpointConfigureActions = new List<Action<EndpointRouteBuilderContext>>();
        }
    }
}

#endif