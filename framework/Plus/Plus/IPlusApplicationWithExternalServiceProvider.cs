using JetBrains.Annotations;
using System;

namespace Plus
{
    public interface IPlusApplicationWithExternalServiceProvider : IPlusApplication
    {
        void Initialize([NotNull] IServiceProvider serviceProvider);
    }
}