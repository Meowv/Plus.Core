#if NETCOREAPP3_1

using Plus.AspNetCore.Mvc.Conventions;

namespace Plus.AspNetCore.Mvc
{
    public class PlusAspNetCoreMvcOptions
    {
        public bool? MinifyGeneratedScript { get; set; }

        public PlusConventionalControllerOptions ConventionalControllers { get; }

        public PlusAspNetCoreMvcOptions()
        {
            ConventionalControllers = new PlusConventionalControllerOptions();
        }
    }
}

#endif