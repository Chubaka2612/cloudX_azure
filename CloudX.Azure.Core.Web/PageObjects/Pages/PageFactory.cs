using CloudX.Azure.Core.Utils;
using CloudX.Azure.Core.Web.Attributes;

namespace CloudX.Azure.Core.Web.PageObjects.Pages
{
    public static class PageFactory
    {
        public static WebPage CreatePageInstance(this Type type)
        {

            var page = (WebPage)Activator.CreateInstance(type);

            var pageAttribute = CommonUtils.GetAttributes<PageAttribute>(page?.GetType());
            if (pageAttribute.Count != 0)
            {
                page.Url = pageAttribute.First().Url;
                page.Name = pageAttribute.First().Name;
            }
            return page;
        }
    }
}