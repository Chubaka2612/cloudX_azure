using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CloudX.Azure.Core.ConfigurationManagement;
using CloudX.Azure.Core.Web.Drivers;

namespace CloudX.Azure.Core.Web.Waiter
{
    public class Wait
    {
        private static WebDriverWait GetWebDriverWait(int timeout, int retryInterval)
        {
            var webDriverWait = new WebDriverWait(new SystemClock(),
               Wdm.Instance, TimeSpan.FromMilliseconds(timeout), TimeSpan.FromMilliseconds(retryInterval));
            webDriverWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
            return webDriverWait;
        }

        public static TResult Until<TResult>(Func<IWebDriver, TResult> condition, int customTimeout = default)
        {
            if (customTimeout == default)
            {
                return GetWebDriverWait(ConfigurationManager.WebPageConfiguration.Timeout,
                    ConfigurationManager.WebPageConfiguration.PollingInterval).Until(condition);
            }
            return GetWebDriverWait(customTimeout, ConfigurationManager.WebPageConfiguration.PollingInterval).Until(condition);
        }

        public static void UntilElementToBePresent(By locator)
        {
            Until(WaitConditions.ElementToBePresent(locator));
        }

        public static void UntilElementToBeVisible(By locator)
        {
            Until(WaitConditions.ElementToBeVisible(locator));
        }

        public static void ALittle()
        {

            Thread.Sleep(500);
        }

        public static void ForMs(int ms)
        {
            Thread.Sleep(ms);
        }

    }
}