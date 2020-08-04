using Plus.Aspects;
using Plus.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Plus.Http.Modeling
{
    public class PlusApiDescriptionModelOptions
    {
        public HashSet<Type> IgnoredInterfaces { get; }

        public PlusApiDescriptionModelOptions()
        {
            IgnoredInterfaces = new HashSet<Type>
            {
                typeof(ITransientDependency),
                typeof(ISingletonDependency),
                typeof(IDisposable),
                typeof(IAvoidDuplicateCrossCuttingConcerns)
            };
        }
    }
}