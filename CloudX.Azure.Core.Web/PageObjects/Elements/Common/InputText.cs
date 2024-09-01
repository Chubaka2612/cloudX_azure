using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Common;
using OpenQA.Selenium;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Common
{
    public class InputText : TextElement, ITextField
    {
        public InputText(By byLocator) : base(byLocator)
        {

        }
        public string Placeholder => GetAttribute("placeholder");

        public new string Value
        {
            get => base.Value;
            set
            {
                Logger.Debug($"Set value into element '{Name}'");
                ClearByTyping();
                SendKeys(value);
            }
        }

        public string AppendValue
        {
            get => base.Value;
            set
            {
                Logger.Debug($"Append value into element '{Name}'");
                SendKeys(value);
            }
        }
    }
}
