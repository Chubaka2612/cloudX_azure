using Cloud.Azure.AwesomePizzaSite.Api.Dto;
using RestSharp;
using SoftAPIClient.Attributes;
using SoftAPIClient.MetaData;

namespace Cloud.Azure.AwesomePizzaSite.Api.Service
{
    [Client(DynamicUrlKey = "BaseApiUrl", DynamicUrlType = typeof(DynamicUrlFromProperty))]
    public interface ICreatePizzaListService
    {
        [Log("Send POST request for creating of pizzas with the following parameters: request-body={0}")]
        [RequestMapping(Method.POST, Path = "")]
        Func<Response> PostPizzaList(
               [Body] PizzaListRequestDto body
            );

        [Log("Send POST request for creating of pizzas with the following parameters: request-body={0}")]
        [RequestMapping(Method.POST, Path = "")]
        Func<Response> PostPizzaList(
              [Body] string body
           );

        [Log("Send Get request for getting info response")]
        [RequestMapping(Method.GET, Path = "")]
        Func<Response> GetPizzaListInfo(
           );

        #region Not supported for testing purposes only

        [Log("Send Delete request")]
        [RequestMapping(Method.DELETE, Path = "")]
        Func<Response> DeletePizzaList();

        [Log("Send Put request")]
        [RequestMapping(Method.PUT, Path = "")]
        Func<Response> PutPizzaList();

        #endregion
    }
}
