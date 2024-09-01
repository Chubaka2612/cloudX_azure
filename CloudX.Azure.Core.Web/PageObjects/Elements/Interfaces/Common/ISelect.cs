using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Common
{
    public interface ISelect : IUiElement
    {
        void ValueSelect(string itemName);
    }
}
