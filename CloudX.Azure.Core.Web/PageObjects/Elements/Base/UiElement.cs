using log4net;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection;
using OpenQA.Selenium.Interactions;
using CloudX.Azure.Core.Web.Drivers;
using CloudX.Azure.Core.Extensions;
using CloudX.Azure.Core.Web.Waiter;
using CloudX.Azure.Core.Web.PageObjects.Pages;
using CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base;
using CloudX.Azure.Core.Web.PageObjects.Pages.Utils;

namespace CloudX.Azure.Core.Web.PageObjects.Elements.Base
{
    public class UiElement : IWebElement, IUiElement
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(UiElement));

        private IWebElement _webElement;

        private string _name;

        public By Locator { get; set; }

        public IUiElement Parent { get; set; }

        public IWebPage Page { get; set; }

        public string TagName => WebElement.TagName;

        public virtual string Text => WebElement.Text;

        public bool Enabled => WebElement.Enabled && WebElement.GetAttribute("disabled") == null && WebElement.GetAttribute("readonly") == null;

        public bool Disabled => WebElement.GetAttribute("disabled") != null;

        public string GetDomAttribute(string attributeName) => WebElement.GetDomAttribute(attributeName);


        public string GetDomProperty(string propertyName) => WebElement.GetDomProperty(propertyName);

        public ISearchContext GetShadowRoot() => WebElement.GetShadowRoot();

        public bool ReadOnly => WebElement.GetAttribute("readonly") != null;

        public bool Selected => WebElement.Selected;

        public Point Location => WebElement.Location;

        public Size Size => WebElement.Size;

        public bool Displayed => WebElement.Displayed;

        public bool Hidden => !WebElement.Displayed;

        public static IJavaScriptExecutor JsExecutor => (IJavaScriptExecutor)Wdm.Instance;

        public IWebDriver WebDriver => Wdm.Instance;

        public bool Exists
        {
            get
            {
                var context = GetSearchContext(Parent);
                var elements = context.FindElements(Locator);
                return elements.Count > 0;
            }
        }

        public string Name
        {
            get
            {
                if (_name != null)
                {
                    return _name;
                }
                return _name = GetType().Name.SplitCamelCase();
            }
            set => _name = value;
        }

        public IList<IWebElement> WebElements
        {
            get
            {
                var context = GetSearchContext(Parent);
                var elements = Wait.Until(driver =>
                {
                    try
                    {
                        return context.FindElements(Locator).ToList();
                    }
                    catch (StaleElementReferenceException ex)
                    {
                        Logger.Debug($"Search context is stale. Retry the search: {ex.Message}");
                        context = GetSearchContext(Parent);
                        return new List<IWebElement>();
                    }
                });
                return elements;
            }
        }

        public IWebElement WebElement
        {
            get
            {
                if (_webElement != null)
                {
                    if (IsNotStale())
                    {
                        return _webElement;
                    }
                    else
                    {
                        _webElement = null;
                    }
                }
                var result = WebElements;

                if (result.Count == 0)
                {
                    throw new Exception($"Can't find element '{this}' with name '{Name}' and locator '{Locator}'");
                }
                return _webElement = result[0];
            }
            set => _webElement = value;
        }

        public bool IsNotStale()
        {
            if (_webElement == null)
            {
                throw new ArgumentException("WebElement is not cached!");
            }
            try
            {
                var displayed = _webElement.Displayed;
                return true;
            }
            catch (WebDriverException e)
            {
                Logger.Debug($"Element {this} with locator {Locator} state is invalid, exception message: {e.Message}");
                return false;
            }
        }

        public UiElement()
        {
        }

        public UiElement(By byLocator)
        {
            Locator = byLocator;
        }

        public IWebElement FindElement(By by) => FindElements(by).First();

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            try
            {
                return Wait.Until(driver => WebElement.FindElements(by));
            }
            catch (Exception e) when (e is StaleElementReferenceException || e is TimeoutException || e is NoSuchElementException)
            {
                return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            }
        }

        public string GetAttribute(string attributeName) => WebElement.GetAttribute(attributeName);

        public string GetCssValue(string propertyName) => WebElement.GetCssValue(propertyName);

        protected static ISearchContext GetSearchContext(IUiElement parent)
        {
            if (parent is IWebPage)
            {
                //using default search context
                return Wdm.Instance.SwitchTo().DefaultContent();
            }

            var parentElement = (UiElement)parent;

            if (parentElement != null && parentElement.WebElement != null)
            {
                // the whole element is a search context
                return parent.WebElement;
            }

            return Wdm.Instance.SwitchTo().DefaultContent();
        }

        public void Clear()
        {
            Logger.Debug($"Clear an element '{Name}'");
            WaitUntilElementToBeClickable();
            WebElement.Clear();
        }

        public UiElement ClearByTyping()
        {
            Logger.Debug($"Clear an element '{Name}' by typing");
            WaitUntilElementToBeClickable();
            WebElement.SendKeys(Keys.Control + "a");
            WebElement.SendKeys(Keys.Backspace);
            return this;
        }

        public virtual UiElement WaitUntilElementToBeClickable()
        {
            Wait.Until(driver => Enabled, customTimeout: 4000);
            return this;
        }

        public virtual UiElement WaitUntilElementToBePresent()
        {
            Wait.Until(driver => Exists);
            return this;
        }

        public virtual UiElement WaitUntilNotStale()
        {
            Wait.Until(driver => IsNotStale());
            return this;
        }

        public virtual UiElement WaitUntilElementToBeVisible()
        {
            Wait.Until(driver => Displayed);
            return this;
        }

        public void VanishedWait()
        {
            Wait.Until(driver => !(Exists && Displayed));
            WebElement = null;
            Wait.ALittle();
        }

        public virtual void Click()
        {
            Logger.Debug($"Click on element '{Name}'");
            WaitUntilElementToBeClickable();
            WebElement.Click();
        }

        public void SendKeys(string text)
        {
            var textToDisplayed = Name.Contains("Password") ? "" : text;
            Logger.Debug($"Send keys '{textToDisplayed}' into element '{Name}'");
            WaitUntilElementToBeClickable();
            ClearByTyping();
            WebElement.SendKeys(text ?? "");
            Blur();
        }

        public void Submit()
        {
            Logger.Debug($"Submit element '{Name}'");
            WaitUntilElementToBeClickable();
            WebElement.Submit();
        }

        public UiElement Blur()
        {
            Logger.Debug($"Blur element {Name}");
            Wdm.ExecuteJavaScript("arguments[0].blur();", WebElement);
            Wait.ALittle();
            return this;
        }

        public virtual void ClickJs()
        {
            Logger.Debug($"Click on element {Name} using JS");
            Wdm.ExecuteJavaScript("arguments[0].click()", WebElement);
        }

        public dynamic ScrollToBottom()
        {
            Wdm.ExecuteJavaScript("arguments[0].scrollIntoView(true);", WebElement);
            Wait.ForMs(1000);
            return this;
        }

        public void MoveToElement()
        {
            Logger.Debug($"Move to element {Name}");
            new Actions(WebDriver)
                .MoveToElement(WebElement)
                .Perform();
        }

        public static object ExecuteStoredJs(string scriptName, params object[] args) =>
          ExecuteStoredJs(scriptName, typeof(UiElement).Assembly, args);

        public static object ExecuteStoredJs(string scriptName, Assembly assembly, params object[] args)
        {
            var script = assembly.GetManifestedResourceContent($"{assembly.GetName().Name}.Javascript.{scriptName}.js");
            return JsExecutor.ExecuteScript(script, args);
        }
    }
}