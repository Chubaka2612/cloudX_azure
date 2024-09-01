using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Elements.Common;
using GTM.Auto.Portal.React.PageObjects.Pages;

namespace Cloud.Azure.AwesomePizzaSite.Web.Pages.ManagePizza
{
    [Page(Name = "Manage Pizza List Page", Url = "/pizzas/add")]
    public class ManagePizzaAddNewPage: BasePage
    {
        [Name("Title Input Text")]
        [FindBy(XPath = "//input[@name='title']")]
        public InputText TitleInputText { get; set; }

        [Name("Submit Button")]
        [FindBy(XPath = "//button[@type='submit']")]
        public Button SubmitButton { get; set; }

    }
}
