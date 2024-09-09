using CloudX.Azure.Core.Utils;
using CloudX.Azure.Core.Extensions;
using Cloud.Azure.AwesomePizzaSite.Data.Model.UI;

namespace Cloud.Azure.AwesomePizzaSite.Data.Service.UI
{
    public class IngredientService
    {
        private static List<string> Ingredients = new() { "Spinach", "Pineapple", "Sausage", "Pepper", "Mozzarella", "Parmesan" };

        public static string EntityPrefixName => "EPAM Auto ";

        public static IngredientModel GenerateIngredient()
        {
            var namePrefix = RandomStringUtils.RandomString(5);

            return new IngredientModel
            {
                Title = EntityPrefixName + namePrefix + " " + Ingredients.GetRandom(),
                Description = "Ingredient #" + namePrefix
            };
        }
    }
}
