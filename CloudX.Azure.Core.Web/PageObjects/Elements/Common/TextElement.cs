using CloudX.Azure.Core.Web.PageObjects.Elements.Base;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Common;
using OpenQA.Selenium;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Common
{
    public class TextElement : UiElement, ITextElement
    {
        public TextElement(By byLocator) : base(byLocator)
        {
        }

        public string Value
        {
            get
            {
                var element = WebElement;
                var text = element.Text ?? "";
                if (string.IsNullOrEmpty(text))
                {
                    text = element.GetAttribute("value");
                }
                return text
                    .Replace("\n", "")
                    .Replace("\r", "");
            }
        }
    }
}
