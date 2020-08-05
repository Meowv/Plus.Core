#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Plus.AspNetCore.Mvc.Conventions
{
    public interface IPlusServiceConvention : IApplicationModelConvention
    {
    }
}

#endif