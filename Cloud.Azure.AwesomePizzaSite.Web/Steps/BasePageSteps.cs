using CloudX.Azure.Core.Web.PageObjects.Pages;
using CloudX.Azure.Core.Web.Waiter;
using CloudX.Azure.Core.Web;
using GTM.Auto.Portal.React.PageObjects.Pages;
using log4net;


namespace Cloud.Azure.AwesomePizzaSite.Web.Steps
{
    public static class BasePageSteps
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BasePageSteps));

        public static T MenuItemSelectAndNavigate<T>(this IWebPage page, string menuItem) where T : WebPage
        {
            Log.Info($"Click on '{menuItem}' item from 'Menu' and navigate to {((BasePage)page).Name}");

            var menu = ((BasePage)page).Menu;
            menu.WaitUntilElementToBePresent();

            Wait.Until(_ => menu.Items.Count > 0);
            Wait.ALittle();
            return menu
                .ItemSelect<T>(menuItem);
        }

        public static T MenuItemSelect<T>(this IWebPage page, string menuItem) where T : WebPage
        {
            Log.Info($"Click on '{menuItem}' item from 'Menu'");

            ((BasePage)page).Menu
               .ItemSelect(menuItem);
            return Ui.GetPage<T>();
        }
    }
}
