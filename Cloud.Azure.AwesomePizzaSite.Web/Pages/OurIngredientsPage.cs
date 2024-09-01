using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Pages.Utils;
using GTM.Auto.Portal.React.PageObjects.Pages;
using OpenQA.Selenium;
using CloudX.Azure.Core.Web.PageObjects.Elements.Composite;
using CloudX.Azure.Core.Web.PageObjects.Elements.Common;
using CloudX.Azure.Core.Utils; 

namespace Cloud.Azure.AwesomePizzaSite.Web.Pages.ManageIngredients
{
    [Page(Name = "Our Ingredients Page", Url = "/our_ingredients")]
    public class OurIngredientsPage : BasePage
    {
        [Name("Ingredient Container")]
        [FindBy(XPath = "//div[contains(@class,'container box')]")]
        public IngredientsContainer IngredientContainer { get; set; }


        public class IngredientsContainer : Container
        {
            public IngredientsContainer(By locator) : base(locator)
            {
            }

            public IList<IngredientCard> IngredientCards => InitBookingCards();

            protected IList<IngredientCard> InitBookingCards()
            {
                var ingredientCards = UiElementsInit.GetChildElements<IngredientCard>(this, By.XPath(".//div[contains(@class,'card-content')]"));
                if (ingredientCards.Count > 0)
                {
                    ingredientCards.ForEach(segment => segment.InitElements());
                }
                return ingredientCards;
            }
        }

        public class IngredientCard : Container
        {
            public IngredientCard(By byLocator) : base(byLocator)
            {
            }

            [Name("Title Text Elememt")]
            [FindBy(XPath = ".//h1")]
            public TextElement TitleTextElement { get; set; }

            [Name("Description Text Elememt")]
            [FindBy(XPath = ".//p")]
            public TextElement DescriptionTextElement { get; set; }

        }
    }
}
