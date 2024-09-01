using CloudX.Azure.Core.Web.PageObjects.Elements.Base;
using OpenQA.Selenium;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Complex.Table
{
    public class Cell : UiElement
    {
        public UiElement Control { get; set; }

        public Cell(By locator) : base(locator)
        {
        }

        public string HeaderText { get; set; }

        public string ContentText { get; set; }

        public virtual string Value
        {
            get
            {
                if (Control != null)
                {
                    return Control.Text;
                }
                if (string.IsNullOrEmpty(ContentText))
                {
                    return Text;
                }
                else
                {
                    return ContentText;
                }
            }
        }

        public void SetControl(UiElement control)
        {
            Control = control;
        }
    }
}
