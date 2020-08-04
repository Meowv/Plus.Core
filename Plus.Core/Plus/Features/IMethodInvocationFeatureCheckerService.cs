using System.Threading.Tasks;

namespace Plus.Features
{
    public interface IMethodInvocationFeatureCheckerService
    {
        Task CheckAsync(
            MethodInvocationFeatureCheckerContext context
        );
    }
}