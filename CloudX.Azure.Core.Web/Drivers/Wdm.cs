using OpenQA.Selenium;
using CloudX.Azure.Core.Enums;
using CloudX.Azure.Core.ConfigurationManagement.Models.Drivers;
using CloudX.Azure.Core.Web.Drivers.Strategy;
using CloudX.Azure.Core.ConfigurationManagement;

namespace CloudX.Azure.Core.Web.Drivers
{
    public static class Wdm
    {
        public static string DriverVersion { get; set; }

        public static DriverType SelectedDriverType { get; set; }

        public static ConfigurationManagement.Models.Drivers.DriverOptions DriverOptions { get; set; }

        public static DriverKiller DriverKiller { get; set; }


        private static readonly ThreadLocal<IWebDriver> DriverInstance;


        private static readonly DriverConfiguration DriverConfiguration;


        private static readonly Dictionary<DriverType, IDriverStrategy> DriverStrategies = new()
        {
            { DriverType.Chrome, new ChromeDriverStrategy() },
            { DriverType.ChromeHeadless, new ChromeHeadlessDriverStrategy() }
        };

        private static readonly Dictionary<DriverType, dynamic> WebDriverOptions = new()
        {
            { DriverType.Chrome, new ChromeDriverStrategy() },
            { DriverType.ChromeHeadless, new ChromeHeadlessDriverStrategy() }
        };

        static Wdm()
        {
            DriverKiller = new DriverKiller();
            DriverInstance = new ThreadLocal<IWebDriver>();
            DriverConfiguration = ConfigurationManager.DriverConfiguration;
            SelectedDriverType = DriverConfiguration.SelectedDriverType;
            DriverOptions = DriverConfiguration.DriverOptions.First(o => o.DriverKey == SelectedDriverType);
            DriverVersion = DriverOptions.DriverVersion;
        }

        public static void Register()
        {
            RegisterLocal();
            DriverInstance.Value.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(120));
        }

        private static void RegisterLocal()
        {
            DriverStrategies[SelectedDriverType].SetDriverConfiguration(DriverVersion);
            DriverInstance.Value = DriverStrategies[SelectedDriverType].GetDriverInstance();
        }

        public static IWebDriver Instance => DriverInstance.Value;

        public static void Quit()
        {
            if (DriverInstance.IsValueCreated && Instance != null)
            {
                Instance.Quit();
            }
        }

        public static void Close()
        {
            if (DriverInstance.IsValueCreated && Instance != null)
            {
                Instance.Close();
                Waiter.Wait.ForMs(2000);
            }
        }

        public static void KillAllRunning()
        {
            DriverKiller.KillRunningDrivers();
        }

        public static object ExecuteJavaScript(string script, params object[] args)
        {
            if (Instance is IJavaScriptExecutor scriptExecutor)
            {
                return scriptExecutor.ExecuteScript(script, args);
            }

            throw new NotSupportedException($"Javascript is not supported by {SelectedDriverType}");
        }
    }
}