using Plus.Collections;
using System;
using System.Collections.Generic;

namespace Plus.Validation
{
    public class PlusValidationOptions
    {
        public List<Type> IgnoredTypes { get; }

        public ITypeList<IObjectValidationContributor> ObjectValidationContributors { get; set; }

        public PlusValidationOptions()
        {
            IgnoredTypes = new List<Type>();
            ObjectValidationContributors = new TypeList<IObjectValidationContributor>();
        }
    }
}