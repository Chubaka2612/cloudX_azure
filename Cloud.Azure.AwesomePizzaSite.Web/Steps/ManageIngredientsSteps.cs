using Cloud.Azure.AwesomePizzaSite.Data.Model;
using Cloud.Azure.AwesomePizzaSite.Web.Pages.ManageIngredients;
using CloudX.Azure.Core.Web.Waiter;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Azure.AwesomePizzaSite.Web.Steps
{
    public static class ManageIngredientsSteps
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ManageIngredientsSteps));

        public static ManageIngredientsAddNewPage AddNewIngredient(this ManageIngredientsAddNewPage manageIngredientsAddNewPage, IngredientModel ingredientModel)
        {
            Log.Info($"Add new ingredient: {ingredientModel} on '{manageIngredientsAddNewPage.Name}'");

            manageIngredientsAddNewPage.TitleInputText.SendKeys(ingredientModel.Title);
            manageIngredientsAddNewPage.DescriptionTextArea.SendKeys(ingredientModel.Description);
            manageIngredientsAddNewPage.SubmitButton.Click();

            return manageIngredientsAddNewPage;
        }
    }
}
