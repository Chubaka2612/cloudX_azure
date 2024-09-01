namespace CloudX.Azure.Core.Web.PageObjects.Pages
{
    public interface IWebPage
    {
        string Url { get; set; }

        string Name { get; set; }

        void Open(IList<string> parameters);

    }
}
