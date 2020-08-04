using System;

namespace Plus.MultiTenancy
{
    [AttributeUsage(AttributeTargets.All)]
    public class IgnoreMultiTenancyAttribute : Attribute
    {

    }
}