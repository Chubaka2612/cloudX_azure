using OpenQA.Selenium;
using System.Reflection;
using CloudX.Azure.Core.Extensions;
using CloudX.Azure.Core.Web.PageObjects.Elements;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base;
using CloudX.Azure.Core.Web.PageObjects.Elements.Composite;
using CloudX.Azure.Core.Web.PageObjects.Extensions;
using CloudX.Azure.Core.Web.Attributes;
using CloudX.Azure.Core.Web.PageObjects.Elements.Base;

namespace CloudX.Azure.Core.Web.PageObjects.Pages.Utils
{
    public static class UiElementsInit
    {
        public static void InitElements(this IUiElement parent)
        {
            var members = parent.GetType().FilterMembers();
            foreach (var member in members)
            {
                if (member.HasType<Container>())
                {
                    InitContainer(member, parent);
                    continue;
                }
                if (member.HasType<UiElement>())
                {
                    InitUiElement(member, parent);
                }
            }
        }

        private static UiElement InitUiElement(MemberInfo member, IUiElement parent)
        {
            var element = (UiElement)member.GetMemberValue(parent);
            if (element == null)
            {
                var locator = member.GetCustomAttribute<FindByAttribute>(false)?.ByLocator;
                var memberType = member.GetMemberType();

                element = (UiElement)(typeof(IWebPage).IsAssignableFrom(parent.GetType())
                    ? UiElementFactory.CreateInstance(memberType, locator)
                    : UiElementFactory.CreateInstance(memberType, locator, parent));

                element.Name = member.GetCustomAttributes<NameAttribute>(false).FirstOrDefault()?.Name;
                element.Page = parent is IWebPage page ? page : ((UiElement)parent).Page;

                member.SetMemberValue(parent, element);
            }
            return element;
        }

        public static Container InitContainer(MemberInfo member, IUiElement parent)
        {
            var container = InitUiElement(member, parent);
            container.InitElements();
            return (Container)container;
        }

        public static IList<T> GetChildElements<T>(UiElement parent, By locator) where T : UiElement
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            return parent.FindElements(locator).Select(e => UiElementFactory.CreateInstance<T>(locator, parent, e)).ToList();
        }
    }
}