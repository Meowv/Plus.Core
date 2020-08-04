using Plus.Domain.Entities;
using System.Linq;

namespace Plus.Auditing
{
    public static class EntityHistorySelectorListExtensions
    {
        public const string AllEntitiesSelectorName = "Plus.Entities.All";

        public static void AddAllEntities(this IEntityHistorySelectorList selectors)
        {
            if (selectors.Any(s => s.Name == AllEntitiesSelectorName))
            {
                return;
            }

            selectors.Add(new NamedTypeSelector(AllEntitiesSelectorName, t => typeof(IEntity).IsAssignableFrom(t)));
        }
    }
}