namespace Plus.Data
{
    public static class PlusCommonDbProperties
    {
        /// <summary>
        /// This table prefix is shared by most of the Plus modules.
        /// You can change it to set table prefix for all modules using this.
        /// 
        /// Default value: "Plus".
        /// </summary>
        public static string DbTablePrefix { get; set; } = "Plus";

        /// <summary>
        /// Default value: null.
        /// </summary>
        public static string DbSchema { get; set; } = null;
    }
}