using OpenQA.Selenium;
using WebDriverManager.DriverConfigs;

namespace CloudX.Azure.Core.Web.Drivers.Strategy
{

    public interface IDriverStrategy
    {
        void SetDriverConfiguration(string version);

        IWebDriver GetDriverInstance();

        IDriverConfig GetDriverConfiguration();

        string GetDriverLatestVersion();
    }
}
