using System;
using System.Linq;

namespace Plus.AspNetCore.Mvc.UI.Theming
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ThemeNameAttribute : Attribute
    {
        public string Name { get; set; }

        public ThemeNameAttribute(string name)
        {
            Name = name;
        }

        public static string GetName(Type themeType)
        {
            return themeType
                       .GetCustomAttributes(true)
                       .OfType<ThemeNameAttribute>()
                       .FirstOrDefault()?.Name ?? themeType.Name;
        }
    }
}