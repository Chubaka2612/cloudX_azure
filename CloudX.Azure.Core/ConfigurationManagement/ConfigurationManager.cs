using Microsoft.Extensions.Configuration;
using System.Xml;
using CloudX.Azure.Core.ConfigurationManagement.Models.Drivers;
using CloudX.Azure.Core.ConfigurationManagement.Models;
using CloudX.Azure.Core.Exceptions;
using CloudX.Azure.Core.ConfigurationManagement.Models.Environments;
using System.Reflection;

namespace CloudX.Azure.Core.ConfigurationManagement
{
    public static class ConfigurationManager
    {
        private static XmlElement _logConfiguration;

        private static IConfiguration _configuration;

        private static DriverConfiguration _driverConfiguration;

        private static WebPageConfiguration _webPageConfiguration;

        public static IConfiguration Configuration => GetConfiguration("appsettings.json");

        public static string SubscriptionId => Configuration["subscriptionId"];


        private static EnvironmentConfiguration _environmentConfiguration;

        public static EnvironmentOptions SelectedEnvironmentOptions
        {
            get
            {
                var configuration = EnvironmentConfiguration;
                return configuration.EnvironmentOptions.First(e => e.EnvironmentName == configuration.SelectedEnvironment);
            }
        }

        public static DriverConfiguration DriverConfiguration
        {
            get
            {
                _driverConfiguration ??= Get<DriverConfiguration>(nameof(DriverConfiguration));
                return _driverConfiguration;
            }
        }

        public static EnvironmentConfiguration EnvironmentConfiguration
        {
            get
            {
                _environmentConfiguration ??= Get<EnvironmentConfiguration>(nameof(EnvironmentConfiguration));
                return _environmentConfiguration;
            }
        }

        public static WebPageConfiguration WebPageConfiguration
        {
            get
            {
                _webPageConfiguration ??= Get<WebPageConfiguration>(nameof(WebPageConfiguration));
                return _webPageConfiguration;
            }
        }


        public static IConfiguration GetConfiguration(string fileName)
        {
            if (_configuration != null)
            {
                return _configuration;
            }
            try
            {
                var directory = AppDomain.CurrentDomain.BaseDirectory;
                _configuration = new ConfigurationBuilder().SetBasePath(directory)
                    .AddJsonFile(fileName)
                    .AddUserSecrets(Assembly.GetCallingAssembly())
                    .Build();

                return _configuration;
            }
            catch (Exception ex)
            {
                throw new InitializationException($"Can't load config file {fileName}:" + ex.Message);
            }
        }

        public static XmlElement GetLogConfiguration
        {
            get
            {
                if (_logConfiguration == null)
                {
                    var log4NetConfig = new XmlDocument();
                    try
                    {
                        log4NetConfig.Load(File.OpenRead("log4net.config"));
                        _logConfiguration = log4NetConfig["log4net"];
                    }
                    catch (Exception ex)
                    {
                        throw new InitializationException($"Can't load config file for log: " + ex.Message);
                    }
                }
                return _logConfiguration;
            }
        }

        private static T Get<T>(string sectionName)
        {
            var section = Configuration.GetSection(sectionName);
            return section.Get<T>();
        }

        public static T Get<T>(string sectionName, string fileName)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(directory);
            configBuilder.AddJsonFile(fileName, true);
            return configBuilder.Build().GetSection(sectionName).Get<T>();
        }
    }
}