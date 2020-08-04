using System;

namespace Plus.Settings
{
    [Serializable]
    public class SettingValue : NameValue
    {
        public SettingValue()
        {

        }

        public SettingValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}