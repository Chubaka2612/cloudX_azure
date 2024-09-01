using OpenQA.Selenium;
using log4net;
using CloudX.Azure.Core.Web.PageObjects.Elements.Composite;
using CloudX.Azure.Core.Web.Drivers;
using CloudX.Azure.Core.ConfigurationManagement;
using CloudX.Azure.Core.Web.Utils;

namespace CloudX.Azure.Core.Web.PageObjects.Pages
{
    public abstract class WebPage : Container, IWebPage
    {
        private string _url;

        private string _name;

        public string EntityId { get; set; }

        protected new static readonly ILog Logger = LogManager.GetLogger(typeof(WebPage));

        private static INavigation Navigator => Wdm.Instance.Navigate();

        public virtual string Url
        {
            get => _url;
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("Url of the page cannot be null");
                }

                _url = UrlHelper.AppendRelativeUrlToBaseUrl(ConfigurationManager.SelectedEnvironmentOptions.BaseWebUrl, value).ToString();
            }
        }

        public new string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentException("Name of the page cannot be null");
        }

        public dynamic Refresh()
        {
            Logger.Debug($"Refresh the page {Url}");
            Navigator.Refresh();
            Waiter.Wait.ALittle();
            return this;
        }

        public void Back()
        {
            Logger.Debug($"Move back from the {Url}");
            Navigator.Back();
        }

        public void Forward()
        {
            Logger.Debug($"Move forward from the {Url}");
            Navigator.Forward();
        }

        public void Open(IList<string> parameters = null)
        {
            var url = parameters == null ? Url : UrlHelper.AppendParametersToUrl(new Uri(Url), parameters).ToString();
            Logger.Debug($"Open the page by url {Url}");

            if (Wdm.Instance.Url.Equals(url, StringComparison.OrdinalIgnoreCase))
            {
                Refresh();
            }
            else
            {
                Wdm.Instance.Navigate().GoToUrl(new Uri(url));
            }
        }

        public new dynamic ScrollToBottom()
        {
            Wdm.ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight)");
            //Let Js completed
            Waiter.Wait.ALittle();
            return this;
        }

        public dynamic ScrollToUp()
        {
            Wdm.ExecuteJavaScript("window.scrollTo(0, -document.body.scrollHeight)");
            //Let Js completed
            Waiter.Wait.ALittle();
            return this;
        }
    }
}