using OpenQA.Selenium;
using CloudX.Azure.Core.Web.PageObjects.Pages;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base;
using CloudX.Azure.Core.Web.PageObjects.Elements.Base;

namespace CloudX.Azure.Core.Web.PageObjects.Elements
{
    public static class UiElementFactory
    {
        public static IUiElement CreateInstance(Type type, By locator = null)
        {
            var constructor = type.GetConstructor(new[] { typeof(By) })
                              ?? throw new MissingMethodException($"Can't find correct constructor to create instance of type {type}"); ;

            return constructor.GetParameters().Length switch
            {
                1 => (UiElement)constructor.Invoke(new object[] { locator }),
                0 => (UiElement)constructor.Invoke(new object[] { }),
                _ => throw new ArgumentOutOfRangeException(nameof(constructor)),
            };
        }

        public static IUiElement CreateInstance(Type type, By locator, IUiElement parent)
        {
            var element = (UiElement)CreateInstance(type, locator);
            element.Parent = parent;
            element.Page = parent is IWebPage page ? page : ((UiElement)parent).Page;
            return element;
        }

        public static T CreateInstance<T>(By locator, IUiElement parent, IWebElement element) where T : IUiElement
        {
            var instance = (T)CreateInstance(typeof(T), locator, parent);
            instance.WebElement = element;
            return instance;
        }

        public static T CreateInstance<T>(By locator, IUiElement parent) where T : IUiElement
        {
            return (T)CreateInstance(typeof(T), locator, parent);
        }
    }
}
