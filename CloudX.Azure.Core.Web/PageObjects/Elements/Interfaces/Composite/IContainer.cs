using CloudX.Azure.Core.Web.PageObjects.Elements.Base;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Composite
{
    public interface IContainer : IUiElement
    {
        T GetUiElement<T>(string elementName) where T : UiElement;
    }
}
