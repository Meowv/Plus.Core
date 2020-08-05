#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Generic;

namespace Plus.AspNetCore.Mvc.ApiExploring
{
    public class PlusRemoteServiceApiDescriptionProviderOptions
    {
        public HashSet<ApiResponseType> SupportedResponseTypes { get; set; } = new HashSet<ApiResponseType>();
    }
}

#endif