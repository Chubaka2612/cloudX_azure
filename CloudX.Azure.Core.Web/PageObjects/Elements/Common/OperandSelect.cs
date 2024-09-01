using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CloudX.Azure.Core.Web.Waiter;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Common;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base;
using CloudX.Azure.Core.Web.PageObjects.Elements.Base;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Common
{
    public class OperandSelect : UiElement, ISelect, ISetValue<string>
    {
        public OperandSelect(By byLocator) : base(byLocator)
        {
        }

        public SelectElement UiSelectElement => new(WebElement);

        public string Value
        {
            get
            {
                try
                {
                    return UiSelectElement.SelectedOption.Text;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
            set => ValueSelect(value);
        }

        public void ValueSelect(string itemName)
        {
            try
            {
                UiSelectElement.SelectByText(itemName);
            }
            catch (NoSuchElementException)
            {
                //Give one more chance to load options
                Wait.ALittle();
                UiSelectElement.SelectByText(itemName);
            }
        }
    }
}
