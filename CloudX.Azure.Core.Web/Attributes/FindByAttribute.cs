using OpenQA.Selenium;

namespace CloudX.Azure.Core.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class FindByAttribute : Attribute
    {
        public By ByLocator { get; private set; }

        public string Id
        {
            set => ByLocator = By.Id(value);
            get => "";
        }

        public string Name
        {
            set => ByLocator = By.Name(value);
            get => "";
        }

        public string ClassName
        {
            set => ByLocator = By.ClassName(value);
            get => "";
        }

        public string Css
        {
            set => ByLocator = By.CssSelector(value);
            get => "";
        }

        public string XPath
        {
            set => ByLocator = By.XPath(value);
            get => "";
        }

        public string Tag
        {
            set => ByLocator = By.TagName(value);
            get => "";
        }

        public string LinkText
        {
            set => ByLocator = By.LinkText(value);
            get => "";
        }

        public string PartialLinkText
        {
            set => ByLocator = By.PartialLinkText(value);
            get => "";
        }
    }
}