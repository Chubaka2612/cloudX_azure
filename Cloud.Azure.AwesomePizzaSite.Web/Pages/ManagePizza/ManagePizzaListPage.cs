using CloudX.Azure.Core.Web.Attributes;
using OpenQA.Selenium;
using CloudX.Azure.Core.Web.PageObjects.Elements.Composite;
using CloudX.Azure.Core.Web.PageObjects.Elements.Common;
using GTM.Auto.Portal.React.PageObjects.Pages;
using CloudX.Azure.Core.Web.PageObjects.Elements.Complex.Table;

namespace Cloud.Azure.AwesomePizzaSite.Web.Pages.ManagePizza
{
    [Page(Name = "Manage Pizza List Page", Url = "/pizzas")]
    public class ManagePizzaListPage: BasePage
    {

        [Name("Add Ingredient Modal")]
        public Modal<AddIngredientModalContent> AddIngredientModal { get; set; }


        [Name("Pizzas Table")]
        [FindBy(XPath = "//table[contains(@class,'table')]")]
        public Table PizzasTable { get; set; }

        public class AddIngredientModalContent : Container
        {
            public AddIngredientModalContent(By locator) : base(locator)
            {
            }

            [Name("Ingredient Select")]
            [FindBy(XPath = ".//select[@name='ingredient_id']")]
            public OperandSelect IngredientSelect { get; set; }

            [Name("Submit Button")]
            [FindBy(XPath = ".//button[@type='submit']")]
            public Button SubmitButton { get; set; }
        }
    }
}
