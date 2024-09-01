using CloudX.Azure.Core.Logging;
using CloudX.Azure.Core.Web.Drivers;
using log4net;
using NUnit.Framework;

namespace Cloud.Azure.AwesomePizzaSite.Web.Tests
{
   
        [SetUpFixture]
        [Parallelizable(ParallelScope.Fixtures)]
        public class GlobalSetup
        {

            [OneTimeSetUp]
            public void BeforeAll()
            {
                LogConfigurator.Configure();
            }

            [OneTimeTearDown]
            public void AfterAll()
            {
                Wdm.KillAllRunning();
            }

        }

        [TestFixture]
        public class BaseTest
        {
            protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseTest));

            [SetUp]
            protected void BeforeEach()
            {
                Log.Debug($"Start driver {Wdm.SelectedDriverType}");
                Wdm.Register();
            }

            [TearDown]
            public void AfterEach()
            {
                Wdm.Instance.Manage().Cookies.DeleteAllCookies();
                Log.Debug($"Quit driver {Wdm.SelectedDriverType}");
                Wdm.Quit();

            }
        }
    }

