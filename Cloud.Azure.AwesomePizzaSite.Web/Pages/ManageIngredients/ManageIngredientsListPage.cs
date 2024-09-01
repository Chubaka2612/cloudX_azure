using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Elements.Complex.Table;
using GTM.Auto.Portal.React.PageObjects.Pages;


namespace Cloud.Azure.AwesomePizzaSite.Web.Pages.ManageIngredients
{
    [Page(Name = "Manage Ingredients List Page", Url = "/ingredients/add")]
    public class ManageIngredientsListPage: BasePage
    {
        [Name("Ingredients List Table")]
        [FindBy(XPath = "//table[contains(@class,'table')]")]
        public Table IngredientsListTable { get; set; }
    }
}
