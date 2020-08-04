using System.Collections.Generic;

namespace Plus.Localization.Json
{
    public class JsonLocalizationFile
    {
        /// <summary>
        /// Culture name; eg : en , en-us, zh-CN
        /// </summary>
        public string Culture { get; set; }

        public Dictionary<string, string> Texts { get; }

        public JsonLocalizationFile()
        {
            Texts = new Dictionary<string, string>();
        }
    }
}