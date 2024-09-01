using OpenQA.Selenium;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base
{
    public interface IUiElement
    {
        string Name { get; set; }

        By Locator { get; set; }

        IWebElement WebElement { get; set; }
    }
}
