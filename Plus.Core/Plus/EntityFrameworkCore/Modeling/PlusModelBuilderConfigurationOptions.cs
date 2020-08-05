using JetBrains.Annotations;

namespace Plus.EntityFrameworkCore.Modeling
{
    public class PlusModelBuilderConfigurationOptions
    {
        [NotNull]
        public string TablePrefix
        {
            get => _tablePrefix;
            set
            {
                Check.NotNull(value, nameof(value), $"{nameof(TablePrefix)} can not be null! Set to empty string if you don't want a table prefix.");
                _tablePrefix = value;
            }
        }
        private string _tablePrefix;

        [CanBeNull]
        public string Schema { get; set; }

        public PlusModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
        {
            Check.NotNull(tablePrefix, nameof(tablePrefix));

            TablePrefix = tablePrefix;
            Schema = schema;
        }
    }
}