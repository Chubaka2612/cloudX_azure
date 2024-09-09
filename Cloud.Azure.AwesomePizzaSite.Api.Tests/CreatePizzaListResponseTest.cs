using Cloud.Azure.AwesomePizzaSite.Api.Dto;
using Cloud.Azure.AwesomePizzaSite.Api.Service;
using Cloud.Azure.AwesomePizzaSite.Data.Meta;
using CloudX.Azure.Core.Utils;
using HtmlAgilityPack;
using NUnit.Framework;
using SoftAPIClient.Core;

namespace Cloud.Azure.AwesomePizzaSite.Api.Tests
{
    public class CreatePizzaListResponseTest : BaseApiTest
    {
        [Test]
        [Category(TestType.WebApi)]
        public void PostPizzaListWithValidDataAndParseResponseTest()
        {
            var pizzaListRequestDto = new PizzaListRequestDto();
            var pizza1 = new Pizza();
            pizza1.Name = "EPAM Auto Marharita " + RandomStringUtils.RandomString(5);
            pizza1.Ingredients = ["Onion", "Tomato", "Chiken"];

            var pizza2 = new Pizza();
            pizza2.Name = "EPAM Auto Neopolitana " + RandomStringUtils.RandomString(5);
            pizza2.Ingredients = ["Cheese", "Olives", "Chiken"];

            pizzaListRequestDto.Pizzas.Add(pizza1);
            pizzaListRequestDto.Pizzas.Add(pizza2);

            var response = RestClient.Instance
                .GetService<ICreatePizzaListService>()
                .PostPizzaList(pizzaListRequestDto)
                .Invoke()
                .ToString();
        
         
            //Parsing
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

           
            // Find all rows in the table
            var rows = htmlDoc.DocumentNode.SelectNodes("//table/tbody/tr");

            VerifyThat
                .NotNull(rows, "Verify rows found in the HTML table");

            foreach (var expectedPizza in pizzaListRequestDto.Pizzas)
            {
                // Find the row that matches the expected pizza name
                var row = rows.FirstOrDefault(r => r.SelectSingleNode("td[1]").InnerText.Contains(expectedPizza.Name.ToString()));
                VerifyThat.NotNull(row, $"Verify Pizza {expectedPizza.Name} found in the HTML");

                // Extract the ingredients from the HTML
                var actualIngredients = row.SelectSingleNode("td[2]/ul")
                                     .SelectNodes("li")
                                     .Select(li => li.InnerText.Trim())
                                     .ToArray();

                VerifyThat.AreEquals(expectedPizza.Ingredients.Count, actualIngredients.Length, $"Mismatch in number of ingredients for pizza {expectedPizza.Name}.");

                // Verify that all expected ingredients are present
                foreach (var ingredient in expectedPizza.Ingredients)
                {
                    VerifyThat.CollectionContains(actualIngredients, [ingredient], $"Verify ingredient {ingredient} is found for pizza {expectedPizza.Name}.");
                }
            }
        }
    }
}

