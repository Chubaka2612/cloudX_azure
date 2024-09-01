using Cloud.Azure.AwesomePizzaSite.Data.Model;
using CloudX.Azure.Core.Utils;
using CloudX.Azure.Core.Extensions;

namespace Cloud.Azure.AwesomePizzaSite.Data.Service
{
    public class PizzaService
    {
        private static List<string> Ingredients = new() { "Margherita", "Pepperoni", "Vegetarian", "Supreme", "Capricciosa ", "Marinara" };

        public static string EntityPrefixName => "EPAM Auto ";

        public static PizzaModel GeneratePizza(List<IngredientModel> ingredients)
        {
            var namePrefix = RandomStringUtils.RandomString(5);

            return new PizzaModel
            {
                Title = EntityPrefixName + namePrefix + " " + Ingredients.GetRandom(),
                Ingredients = ingredients
            };
        }
    }
}
