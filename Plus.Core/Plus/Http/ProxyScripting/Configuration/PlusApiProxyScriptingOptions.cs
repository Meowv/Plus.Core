using System;
using System.Collections.Generic;

namespace Plus.Http.ProxyScripting.Configuration
{
    public class PlusApiProxyScriptingOptions
    {
        public IDictionary<string, Type> Generators { get; }

        public PlusApiProxyScriptingOptions()
        {
            Generators = new Dictionary<string, Type>();
        }
    }
}