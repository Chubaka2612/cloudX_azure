using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Elements.Common;
using CloudX.Azure.Core.Web.PageObjects.Elements.Composite;
using GTM.Auto.Portal.React.PageObjects.Pages;
using OpenQA.Selenium;
using CloudX.Azure.Core.Web.PageObjects.Pages.Utils;
using CloudX.Azure.Core.Utils;

namespace Cloud.Azure.AwesomePizzaSite.Web.Pages
{
    [Page(Name = "Pizza Catalog Page", Url = "/")]
    public class PizzaCatalogPage : BasePage
    {
        [Name("Pizza Card Columns Container")]
        [FindBy(XPath = "//div[@class='columns']")]
        public PizzaCardColumnsContainer PizzaCardColumnsContainer { get; set; }

        [Name("Clear Sorting Button")]
        [FindBy(XPath = "//a[contains(@class,'is-small')][1]")]
        public Link ClearSortingButton { get; set; }

        [Name("ASC Sorting Button")]
        [FindBy(XPath = "//a[contains(@class,'is-small')][2]")]
        public Link AscSortingButton { get; set; }

        [Name("DESC Sorting Button")]
        [FindBy(XPath = "//a[contains(@class,'is-small')][3]")]
        public Link DescSortingButton { get; set; }

        public PizzaCatalogPage Open()
        {
            base.Open();

            return this;
        }
    }


    public class PizzaCardColumnsContainer : Container
    {
        public PizzaCardColumnsContainer(By locator) : base(locator)
        {
        }

        public IList<PizzaCardColumn> PizzaCardColumns => InitPizzaCardColumns();

        protected IList<PizzaCardColumn> InitPizzaCardColumns()
        {
            var pizzaCardColumns = UiElementsInit.GetChildElements<PizzaCardColumn>(this, By.XPath(".//div[contains(@class, 'column')]"));
            if (pizzaCardColumns.Count > 0)
            {
                pizzaCardColumns.ForEach(segment => segment.InitElements());
            }
            return pizzaCardColumns;
        }
    }

    public class PizzaCardColumn : Container
    {

        public PizzaCardColumn(By locator) : base(locator)
        {
        }

        public IList<PizzaCard> PizzaCards => InitPizzaCards();

        protected IList<PizzaCard> InitPizzaCards()
        {
            var pizzaCards = UiElementsInit.GetChildElements<PizzaCard>(this, By.XPath(".//div[contains(@class, 'card')]"));
            if (pizzaCards.Count > 0)
            {
                pizzaCards.ForEach(segment => segment.InitElements());
            }
            return pizzaCards;
        }
    }

    public class PizzaCard : Container
    {
        public PizzaCard(By byLocator) : base(byLocator)
        {
        }

        [Name("Title")]
        [FindBy(Tag = "h1")]
        public TextElement TitleTextElement { get; set; }
    }
}
