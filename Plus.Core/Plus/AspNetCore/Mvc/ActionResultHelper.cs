#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Plus.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.AspNetCore.Mvc
{
    public static class ActionResultHelper
    {
        public static List<Type> ObjectResultTypes { get; }

        static ActionResultHelper()
        {
            ObjectResultTypes = new List<Type>
            {
                typeof(JsonResult),
                typeof(ObjectResult),
                typeof(NoContentResult)
            };
        }

        public static bool IsObjectResult(Type returnType, params Type[] excludeTypes)
        {
            returnType = AsyncHelper.UnwrapTask(returnType);

            if (!excludeTypes.IsNullOrEmpty() && excludeTypes.Any(t => t.IsAssignableFrom(returnType)))
            {
                return false;
            }

            if (!typeof(IActionResult).IsAssignableFrom(returnType))
            {
                return true;
            }

            return ObjectResultTypes.Any(t => t.IsAssignableFrom(returnType));
        }
    }
}

#endif