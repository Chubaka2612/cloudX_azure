using CloudX.Azure.Core.Web.PageObjects.Pages;
using CloudX.Azure.Core.Web.PageObjects.Pages.Utils;


namespace CloudX.Azure.Core.Web
{
    public static class Ui
    {
        public static T GetPage<T>() where T : WebPage
        {
            var page = typeof(T).CreatePageInstance();
            page.InitElements();
          
            return (T)page;
        }
    }
}
