using Microsoft.Extensions.DependencyInjection;
using Plus.DependencyInjection;
using System;

namespace Plus.MongoDB.DependencyInjection
{
    public class PlusMongoDbContextRegistrationOptions : PlusCommonDbContextRegistrationOptions, IPlusMongoDbContextRegistrationOptionsBuilder
    {
        public PlusMongoDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
            : base(originalDbContextType, services)
        {
        }
    }
}