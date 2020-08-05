#if NETCOREAPP3_1

using Microsoft.AspNetCore.Builder;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.RequestLocalization
{
    public interface IPlusRequestLocalizationOptionsProvider
    {
        void InitLocalizationOptions(Action<RequestLocalizationOptions> optionsAction = null);

        RequestLocalizationOptions GetLocalizationOptions();

        Task<RequestLocalizationOptions> GetLocalizationOptionsAsync();
    }
}

#endif