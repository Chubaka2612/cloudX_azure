using CloudX.Azure.Core.Web.PageObjects.Elements.Common;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Composite;
using OpenQA.Selenium;
using CloudX.Azure.Core.Web.Attributes;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Composite
{
    public class Modal<T> : Container, IModal<T> where T : IContainer
    {
        protected virtual By ModalLocator { get; set; } = By.CssSelector(".modal");

        public Modal(By locator) : base(locator)
        {
            Locator = locator ?? ModalLocator;
        }

        [Name("Title")]
        [FindBy(Css = ".title")]
        public virtual TextElement Title { get; set; }

        [FindBy(Css = ".modal-content")]
        public virtual T Content { get; set; }
    }

    public class Modal : Modal<Container>
    {
        public Modal(By locator) : base(locator)
        {
        }
    }
}
