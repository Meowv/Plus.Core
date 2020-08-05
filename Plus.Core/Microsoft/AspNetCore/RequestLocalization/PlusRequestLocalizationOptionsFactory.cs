#if NETCOREAPP3_1

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Plus.Options;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.RequestLocalization
{
    public class PlusRequestLocalizationOptionsFactory : PlusOptionsFactory<RequestLocalizationOptions>
    {
        private readonly IPlusRequestLocalizationOptionsProvider _PlusRequestLocalizationOptionsProvider;

        public PlusRequestLocalizationOptionsFactory(
            IPlusRequestLocalizationOptionsProvider PlusRequestLocalizationOptionsProvider,
            IEnumerable<IConfigureOptions<RequestLocalizationOptions>> setups,
            IEnumerable<IPostConfigureOptions<RequestLocalizationOptions>> postConfigures)
            : base(
                setups,
                postConfigures)
        {
            _PlusRequestLocalizationOptionsProvider = PlusRequestLocalizationOptionsProvider;
        }

        public override RequestLocalizationOptions Create(string name)
        {
            return _PlusRequestLocalizationOptionsProvider.GetLocalizationOptions();
        }
    }
}

#endif