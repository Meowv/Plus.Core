namespace Plus.Domain.Entities.Events.Distributed
{
    public class PlusDistributedEntityEventOptions
    {
        public IAutoEntityDistributedEventSelectorList AutoEventSelectors { get; }

        public EtoMappingDictionary EtoMappings { get; set; }

        public PlusDistributedEntityEventOptions()
        {
            AutoEventSelectors = new AutoEntityDistributedEventSelectorList();
            EtoMappings = new EtoMappingDictionary();
        }
    }
}