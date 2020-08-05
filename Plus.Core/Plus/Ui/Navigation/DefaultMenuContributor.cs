using Plus.UI.Navigation.Localization.Resource;
using System.Threading.Tasks;

namespace Plus.UI.Navigation
{
    public class DefaultMenuContributor : IMenuContributor
    {
        public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            Configure(context);
            return Task.CompletedTask;
        }

        protected virtual void Configure(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<PlusUiNavigationResource>();

            if (context.Menu.Name == StandardMenus.Main)
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(
                        DefaultMenuNames.Application.Main.Administration,
                        l["Menu:Administration"],
                        icon: "fa fa-wrench"
                    )
                );
            }
        }
    }
}