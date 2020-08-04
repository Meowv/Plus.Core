using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Plus.Features
{
    public interface IFeatureValueProvider
    {
        string Name { get; }

        Task<string> GetOrNullAsync([NotNull] FeatureDefinition feature);
    }
}