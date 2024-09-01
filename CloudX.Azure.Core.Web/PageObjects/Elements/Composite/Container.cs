using OpenQA.Selenium;
using System.Reflection;
using CloudX.Azure.Core.Extensions;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Composite;
using CloudX.Azure.Core.Web.PageObjects.Extensions;
using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Elements.Base;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Composite
{
    public class Container : UiElement, IContainer
    {
        public Container(By byLocator) : base(byLocator)
        { }

        public Container() { }

        public virtual T GetUiElement<T>(string elementName) where T : UiElement
        {
            var containersMember = GetType().FilterMembers()
               .Where(m => m.HasType<UiElement>())
               .ToList();
            var member = containersMember
                .First(m => m.GetCustomAttribute<NameAttribute>(false).Name.Equals(elementName, StringComparison.OrdinalIgnoreCase));
            if (member == null)
            {
                throw new NotFoundException($"Could not find element of type '{typeof(T)}' with name '{elementName}' in {Name}");
            }
            return (T)member.GetMemberValue(this);
        }
    }
}
