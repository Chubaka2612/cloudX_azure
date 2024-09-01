using OpenQA.Selenium;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Common
{
    public class Link : TextElement
    {
        public Link(By byLocator) : base(byLocator) { }

        public string Alt() => GetAttribute("alt");

        public string Ref() => GetAttribute("href");
    }
}
