namespace Plus.AspNetCore.Mvc.UI.Theming
{
    public class PlusThemingOptions
    {
        public ThemeDictionary Themes { get; }

        public string DefaultThemeName { get; set; }

        public PlusThemingOptions()
        {
            Themes = new ThemeDictionary();
        }
    }
}
