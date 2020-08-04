using System.Reflection;

namespace Plus.Features
{
    public class MethodInvocationFeatureCheckerContext
    {
        public MethodInfo Method { get; }

        public MethodInvocationFeatureCheckerContext(MethodInfo method)
        {
            Method = method;
        }
    }
}