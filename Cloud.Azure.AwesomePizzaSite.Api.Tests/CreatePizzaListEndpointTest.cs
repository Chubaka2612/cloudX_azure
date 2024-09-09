using Cloud.Azure.AwesomePizzaSite.Api.Dto;
using Cloud.Azure.AwesomePizzaSite.Api.Service;
using Cloud.Azure.AwesomePizzaSite.Data.Meta;
using CloudX.Azure.Core.Utils;
using NUnit.Framework;
using SoftAPIClient.Core;
using System.Net;

namespace Cloud.Azure.AwesomePizzaSite.Api.Tests
{
    public class CreatePizzaListEndpointTest: BaseApiTest
    {
        [Test]
        [Category(TestType.WebApi)]
        public void PostPizzaListWithValidDataAndGet200ResponseTest()
        {
            var pizzaListRequestDto = new PizzaListRequestDto();
            var pizza = new Pizza();
            pizza.Name = "EPAM Auto Marharita " + RandomStringUtils.RandomString(5);
            pizza.Ingredients = ["Onion", "Tomato", "Chiken"];
            pizzaListRequestDto.Pizzas.Add(pizza);

            RestClient.Instance
               .GetService<ICreatePizzaListService>()
               .PostPizzaList(pizzaListRequestDto)
               .Invoke()
               .HttpStatusCode.ToString()
               .ShouldContain(HttpStatusCode.OK.ToString());
        }

        [Test]
        [Category(TestType.WebApi)]
        public void GetPizzaListAndGet200ResponseTest()
        {
            RestClient.Instance
               .GetService<ICreatePizzaListService>()
               .GetPizzaListInfo()
               .Invoke()
               .HttpStatusCode.ToString()
               .ShouldContain(HttpStatusCode.OK.ToString());
        }

        [Test]
        [Category(TestType.WebApi)]
        public void PutPizzaListAndGet405ResponseTest()
        {
            RestClient.Instance
               .GetService<ICreatePizzaListService>()
               .PutPizzaList()
               .Invoke()
               .HttpStatusCode.ToString()
               .ShouldContain(HttpStatusCode.MethodNotAllowed.ToString());
        }

        [Test]
        [Category(TestType.WebApi)]
        public void DeletePizzaListAndGet405ResponseTest()
        {
            RestClient.Instance
                .GetService<ICreatePizzaListService>()
                .DeletePizzaList()
                .Invoke()
                .HttpStatusCode.ToString()
                .ShouldContain(HttpStatusCode.MethodNotAllowed.ToString());
        }
        
        [Test]
        [Category(TestType.WebApi)]
        public void PostPizzaListWithEmptyPizzaTitleAndGet200ResponseTest()
        {
            var pizzaListRequestDto = new PizzaListRequestDto();
            var pizza = new Pizza();
            pizza.Name = string.Empty;
            pizza.Ingredients = ["Onion", "Tomato", "Chiken"];
            pizzaListRequestDto.Pizzas.Add(pizza);

            var response = RestClient.Instance
                 .GetService<ICreatePizzaListService>()
                 .PostPizzaList(pizzaListRequestDto)
                 .Invoke().HttpStatusCode.ToString()
                 .ShouldContain(HttpStatusCode.OK.ToString());
        }

        [Test]
        [Category(TestType.WebApi)]
        public void PostPizzaListWithEmptyPizzaIngredientsAndGet200ResponseTest()
        {
            var pizzaListRequestDto = new PizzaListRequestDto();
            var pizza = new Pizza();
            pizza.Name = "EPAM Auto Marharita " + RandomStringUtils.RandomString(5);
         
            var response = RestClient.Instance
                 .GetService<ICreatePizzaListService>()
                 .PostPizzaList(pizzaListRequestDto)
                 .Invoke().HttpStatusCode.ToString()
                 .ShouldContain(HttpStatusCode.OK.ToString());
        }

        [Test]
        [Category(TestType.WebApi)]
        public void PostEmptyPizzaListAndGet404ResponseTest()
        {
           var response = RestClient.Instance
                .GetService<ICreatePizzaListService>()
                .PostPizzaList("{}")
                .Invoke();

            response.HttpStatusCode.ToString()
               .ShouldContain(HttpStatusCode.BadRequest.ToString());
            response.ResponseBodyString.Contains("Request body must have a 'Pizzas' key");
        }

        [Test]
        [Category(TestType.WebApi)]
        public void PostPizzaListWithIncorrectPizzaTitleAndGet404ResponseTest()
        {
            var pizzaListRequestDto = new PizzaListRequestDto();
            var pizza = new Pizza();
            pizza.Name = RandomStringUtils.RandomNumeric(2,3);
            pizza.Ingredients = ["Onion", "Tomato", "Chiken"];
            pizzaListRequestDto.Pizzas.Add(pizza);

            var response = RestClient.Instance
                 .GetService<ICreatePizzaListService>()
                 .PostPizzaList(pizzaListRequestDto)
                 .Invoke();

            response.HttpStatusCode.ToString()
               .ShouldContain(HttpStatusCode.BadRequest.ToString());
            response.ResponseBodyString.ShouldContain("Each pizza must have a string 'Name'");
        }

        [Test]
        [Category(TestType.WebApi)]
        public void PostPizzaListWithIncorrectPizzaIngredientAndGet404ResponseTest()
        {
            var pizzaListRequestDto = new PizzaListRequestDto();
            var pizza = new Pizza();
            pizza.Name = "EPAM Auto Marharita " + RandomStringUtils.RandomString(5);
            pizza.Ingredients = [123, "Tomato", "Chiken"];
            pizzaListRequestDto.Pizzas.Add(pizza);

            var response = RestClient.Instance
                 .GetService<ICreatePizzaListService>()
                 .PostPizzaList(pizzaListRequestDto)
                 .Invoke();

            response.HttpStatusCode.ToString()
               .ShouldContain(HttpStatusCode.BadRequest.ToString());
            response.ResponseBodyString.ShouldContain("Each pizza must have a list of strings 'Ingredients'");
        }
    }
}

