using Microsoft.Extensions.DependencyInjection;
using Plus.DependencyInjection;
using System;

namespace Plus.MemoryDb.DependencyInjection
{
    public class PlusMemoryDbContextRegistrationOptions : PlusCommonDbContextRegistrationOptions, IPlusMemoryDbContextRegistrationOptionsBuilder
    {
        public PlusMemoryDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
            : base(originalDbContextType, services)
        {
        }
    }
}