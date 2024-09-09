using CloudX.Azure.Core.ConfigurationManagement;
using SoftAPIClient.Core.Interfaces;


namespace Cloud.Azure.AwesomePizzaSite.Api
{
    internal class DynamicUrlFromProperty: IDynamicUrl
    {
        public string GetUrl(string key)
        {
            var value = ConfigurationManager.BaseApiUrl;
            if (value != null)
            {
                var stringValue = value.ToString();
                return stringValue.EndsWith("/") ? stringValue.Remove(stringValue.Length - 1) : stringValue;
            }
            throw new Exception($"Can't find configuration property '{key}'");
        }
    }
}
