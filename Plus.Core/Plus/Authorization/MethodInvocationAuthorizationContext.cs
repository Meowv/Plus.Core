using System.Reflection;

namespace Plus.Authorization
{
    public class MethodInvocationAuthorizationContext
    {
        public MethodInfo Method { get; }

        public MethodInvocationAuthorizationContext(MethodInfo method)
        {
            Method = method;
        }
    }
}