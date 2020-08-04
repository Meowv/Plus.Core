using System.Collections.Generic;

namespace Plus.Data
{
    public interface IHasExtraProperties
    {
        Dictionary<string, object> ExtraProperties { get; }
    }
}