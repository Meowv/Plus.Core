﻿using System.Collections.Generic;

namespace Plus.Validation.StringValues
{
    public interface ISelectionStringValueItemSource
    {
        ICollection<ISelectionStringValueItem> Items { get; }
    }
}