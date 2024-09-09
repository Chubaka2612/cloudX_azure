using CloudX.Azure.Core.Utils;
using CloudX.Azure.Core.Extensions;
using Cloud.Azure.AwesomePizzaSite.Data.Model.UI;

namespace Cloud.Azure.AwesomePizzaSite.Data.Service.UI
{
    public class PizzaService
    {
        private static List<string> Titles = new() { "Margherita", "Pepperoni", "Vegetarian", "Supreme", "Capricciosa ", "Marinara" };

        public static string EntityPrefixName => "EPAM Auto ";

        public static PizzaModel GeneratePizza(List<IngredientModel> ingredients)
        {
            var namePrefix = RandomStringUtils.RandomString(5);

            return new PizzaModel
            {
                Title = EntityPrefixName + namePrefix + " " + Titles.GetRandom(),
                Ingredients = ingredients
            };
        }
    }
}
