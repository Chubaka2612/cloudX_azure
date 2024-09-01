namespace CloudX.Azure.Core.Web.Utils
{
    public class UrlHelper
    {
        public static Uri AppendParametersToUrl(Uri uri, IList<string> parameters)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("Uri can't be null");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters should be provided");
            }

            return new Uri(parameters.Aggregate(uri.AbsoluteUri, (param, path) =>
                $"{param.TrimEnd('/')}/{path.TrimStart('/')}"));
        }

        public static Uri AppendRelativeUrlToBaseUrl(Uri baseUri, string relativeUri)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("Base url can't be null");
            }

            if (relativeUri == null)
            {
                throw new ArgumentNullException("Relative url can't be null");
            }

            var uri = baseUri.AbsoluteUri;
            return new Uri($"{uri.TrimEnd('/')}/{relativeUri.TrimStart('/')}".TrimEnd('/'));
        }
    }
}