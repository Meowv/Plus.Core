using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Plus.DynamicProxy
{
    public interface IPlusMethodInvocation
    {
        object[] Arguments { get; }

        IReadOnlyDictionary<string, object> ArgumentsDictionary { get; }

        Type[] GenericArguments { get; }

        object TargetObject { get; }

        MethodInfo Method { get; }

        object ReturnValue { get; set; }

        Task ProceedAsync();
    }
}