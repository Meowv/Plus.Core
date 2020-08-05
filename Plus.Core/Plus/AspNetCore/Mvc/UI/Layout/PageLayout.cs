using Plus.DependencyInjection;

namespace Plus.AspNetCore.Mvc.UI.Layout
{
    public class PageLayout : IPageLayout, IScopedDependency
    {
        public ContentLayout Content { get; }

        public PageLayout()
        {
            Content = new ContentLayout();
        }
    }
}