﻿using JetBrains.Annotations;

namespace Plus.BlobStoring
{
    public static class BlobProviderSelectorExtensions
    {
        public static IBlobProvider Get<TContainer>(
            [NotNull] this IBlobProviderSelector selector)
        {
            Check.NotNull(selector, nameof(selector));

            return selector.Get(BlobContainerNameAttribute.GetContainerName<TContainer>());
        }
    }
}