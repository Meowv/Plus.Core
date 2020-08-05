#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Plus.DependencyInjection;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.Validation
{
    public class PlusValidationActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IModelStateValidator _validator;

        public PlusValidationActionFilter(IModelStateValidator validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //TODO: Configuration to disable validation for controllers..?

            if (!context.ActionDescriptor.IsControllerAction() ||
                !context.ActionDescriptor.HasObjectResult())
            {
                await next();
                return;
            }

            _validator.Validate(context.ModelState);
            await next();
        }
    }
}

#endif