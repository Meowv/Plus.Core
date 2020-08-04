using System.Reflection;

namespace Plus.Validation
{
    public class MethodInvocationValidationContext : PlusValidationResult
    {
        public object TargetObject { get; }

        public MethodInfo Method { get; }

        public object[] ParameterValues { get; }

        public ParameterInfo[] Parameters { get; }

        public MethodInvocationValidationContext(object targetObject, MethodInfo method, object[] parameterValues)
        {
            TargetObject = targetObject;
            Method = method;
            ParameterValues = parameterValues;
            Parameters = method.GetParameters();
        }
    }
}