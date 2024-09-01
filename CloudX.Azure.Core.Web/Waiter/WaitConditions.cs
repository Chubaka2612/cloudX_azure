using OpenQA.Selenium;

namespace CloudX.Azure.Core.Web.Waiter
{
    public static class WaitConditions
    {
        public static Func<IWebDriver, IWebElement> ElementToBeClickable(By locator, ISearchContext context = default)
        {

            return (driver) =>
            {
                var element = ElementIfVisible(context?.FindElement(locator) ?? driver.FindElement(locator));
                try
                {
                    if (element != null && element.Enabled)
                    {
                        return element;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, bool> ElementToBePresent(By locator, ISearchContext context = default)
        {
            return driver =>
            {
                try
                {
                    var element = context?.FindElement(locator) ?? driver.FindElement(locator);
                    return true;
                }
                catch (WebDriverException)
                {
                    return false;
                }
            };
        }

        public static Func<IWebDriver, IWebElement> ElementToBeVisible(By locator, ISearchContext context = default)
        {
            return (driver) =>
            {
                try
                {
                    return ElementIfVisible(context?.FindElement(locator) ?? driver.FindElement(locator));
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, bool> ElementToBeInvisible(By locator, ISearchContext context = default)
        {
            return (driver) =>
            {
                try
                {
                    var count = context?.FindElements(locator).Count ?? driver.FindElements(locator).Count;
                    return count == 0;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            };
        }

        private static IWebElement ElementIfVisible(IWebElement element)
        {
            return element.Displayed ? element : null;
        }
    }
}

