using JetBrains.Annotations;
using System.Collections.Generic;

namespace Plus.UI.Navigation
{
    public class PlusNavigationOptions
    {
        [NotNull]
        public List<IMenuContributor> MenuContributors { get; }

        public PlusNavigationOptions()
        {
            MenuContributors = new List<IMenuContributor>();
        }
    }
}