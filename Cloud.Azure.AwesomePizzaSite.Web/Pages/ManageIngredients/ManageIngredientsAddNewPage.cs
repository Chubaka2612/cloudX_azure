using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Elements.Common;
using GTM.Auto.Portal.React.PageObjects.Pages;


namespace Cloud.Azure.AwesomePizzaSite.Web.Pages.ManageIngredients
{
    [Page(Name = "Manage Ingredients Add New Page", Url = "/ingredients")]
    public class ManageIngredientsAddNewPage : BasePage
    {
        [Name("Title Input Text")]
        [FindBy(XPath = "//input[@name='title']")]
        public InputText TitleInputText { get; set; }

        [Name("Description Text Area")]
        [FindBy(XPath = "//textarea[@name='description']")]
        public InputText DescriptionTextArea { get; set; }

        [Name("Submit Button")]
        [FindBy(XPath = "//button[@type='submit']")]
        public Button SubmitButton { get; set; }
    }
}
