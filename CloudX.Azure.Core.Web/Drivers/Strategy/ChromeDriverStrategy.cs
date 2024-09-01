
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager;
using WebDriverManager.Helpers;


namespace CloudX.Azure.Core.Web.Drivers.Strategy
{
    public class ChromeDriverStrategy : IDriverStrategy
    {
        private readonly IDriverConfig _chromeConfiguration;

        public ChromeDriverStrategy()
        {
            _chromeConfiguration = new ChromeConfig();
        }

        public IDriverConfig GetDriverConfiguration() => _chromeConfiguration;

        public IWebDriver GetDriverInstance()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArguments("--start-maximized");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArguments("--incognito");
            chromeOptions.AddArguments("--disable-infobars");
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions, TimeSpan.FromMinutes(3));
        }

        public string GetDriverLatestVersion() => _chromeConfiguration.GetLatestVersion();

        public void SetDriverConfiguration(string version)
        {
            string versionToSetup;

            if ("MatchingBrowser".Equals(version, StringComparison.OrdinalIgnoreCase))
                versionToSetup = VersionResolveStrategy.MatchingBrowser;
            else if (string.IsNullOrEmpty(version) || "Latest".Equals(version, StringComparison.OrdinalIgnoreCase))
                versionToSetup = VersionResolveStrategy.Latest;
            else
                versionToSetup = version;

            new DriverManager().SetUpDriver(_chromeConfiguration, versionToSetup);
        }
    }
}