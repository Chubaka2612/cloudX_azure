using Cloud.Azure.AwesomePizzaSite.Data.Model.UI;
using Cloud.Azure.AwesomePizzaSite.Web.Pages.ManagePizza;
using CloudX.Azure.Core.Extensions;
using CloudX.Azure.Core.Web.Steps;
using log4net;


namespace Cloud.Azure.AwesomePizzaSite.Web.Steps
{
    public static class ManagePizzasSteps
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ManagePizzasSteps));

        public static ManagePizzaAddNewPage AddNewPizza(this ManagePizzaAddNewPage managePizaAddNewPage, PizzaModel pizzaModel)
        {
            Log.Info($"Add new pizza: {pizzaModel} on '{managePizaAddNewPage.Name}'");

            managePizaAddNewPage.TitleInputText.SendKeys(pizzaModel.Title);
            managePizaAddNewPage.SubmitButton.Click();

            return managePizaAddNewPage;
        }

        public static ManagePizzaListPage AddIngredientsToPizza(this ManagePizzaListPage managePizzaListNewPage, PizzaModel pizzaModel)
        {
            // Log.Info($"Add new ingredients: {pizzaModel.Ingredients.ToJoinString()} on '{managePizzaListNewPage.Name}'");

            foreach (var ingredient in pizzaModel.Ingredients)
            {
                managePizzaListNewPage.PizzasTable.CellClick(ElementTextReference.Table.Pizza.Actions,
                    ElementTextReference.Table.Pizza.Title,
                    pizzaModel.Title);
                managePizzaListNewPage.AddIngredientModal.Content.IngredientSelect.ValueSelect(ingredient.Title);
                managePizzaListNewPage.AddIngredientModal.Content.SubmitButton.Click();
            }


            return managePizzaListNewPage;
        }
    }
}
