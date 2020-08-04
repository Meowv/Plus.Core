using System;
using System.Collections.Generic;

namespace Plus.Data
{
    public class PlusDataFilterOptions
    {
        public Dictionary<Type, DataFilterState> DefaultStates { get; }

        public PlusDataFilterOptions()
        {
            DefaultStates = new Dictionary<Type, DataFilterState>();
        }
    }
}