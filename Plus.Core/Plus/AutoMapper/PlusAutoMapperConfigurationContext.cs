using AutoMapper;
using System;

namespace Plus.AutoMapper
{
    public class PlusAutoMapperConfigurationContext : IPlusAutoMapperConfigurationContext
    {
        public IMapperConfigurationExpression MapperConfiguration { get; }
        public IServiceProvider ServiceProvider { get; }

        public PlusAutoMapperConfigurationContext(
            IMapperConfigurationExpression mapperConfigurationExpression,
            IServiceProvider serviceProvider)
        {
            MapperConfiguration = mapperConfigurationExpression;
            ServiceProvider = serviceProvider;
        }
    }
}