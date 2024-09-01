using CloudX.Azure.Core.Web.PageObjects.Pages;
using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Elements.Composite;
using OpenQA.Selenium;
using CloudX.Azure.Core.Web;
using CloudX.Azure.Core.Web.PageObjects.Elements.Common;
using CloudX.Azure.Core.Web.PageObjects.Pages.Utils;


namespace GTM.Auto.Portal.React.PageObjects.Pages
{
    [Page(Name = "Base Page")]
    public abstract class BasePage : WebPage
    {

        [Name("Main Menu")]
        [FindBy(XPath = "//*[@class='menu']")]
        public Menu Menu { get; set; }
    }

    public class Menu : Container
    {
        public Menu(By byLocator) : base(byLocator)
        {
        }

      
        public IList<Link> Items => UiElementsInit.GetChildElements<Link>(this, By.XPath(".//ul[@class='menu-list']//li/a"));

        public T ItemSelect<T>(string itemName) where T : WebPage
        {
            Items.First(item => item.Ref().Contains(itemName)).Click();
            var page = Ui.GetPage<T>();
            return page;
        }

        public void ItemSelect(string itemName)
        {
            Items.First(item => item.Text.Contains(itemName)).Click();
        }
    }
}