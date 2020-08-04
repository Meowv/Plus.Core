using System;

namespace Plus.Auditing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class AuditedAttribute : Attribute
    {

    }
}