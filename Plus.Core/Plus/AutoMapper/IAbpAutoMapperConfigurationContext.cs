using AutoMapper;
using System;

namespace Plus.AutoMapper
{
    public interface IPlusAutoMapperConfigurationContext
    {
        IMapperConfigurationExpression MapperConfiguration { get; }

        IServiceProvider ServiceProvider { get; }
    }
}