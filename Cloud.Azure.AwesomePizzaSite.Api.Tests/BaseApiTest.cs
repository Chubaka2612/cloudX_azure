using CloudX.Azure.Core.Logging;
using log4net;
using NUnit.Framework;
using SoftAPIClient.Core;
using SoftAPIClient.Implementations;
using SoftAPIClient.RestSharpNewtonsoft;

namespace Cloud.Azure.AwesomePizzaSite.Api.Tests
{
    [SetUpFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class GlobalSetup
    {

        [OneTimeSetUp]
        public void BeforeAll()
        {
            LogConfigurator.Configure();
            InitRestClient();
        }

        private static void InitRestClient()
        {
            var log = LogManager.GetLogger(typeof(GlobalSetup));
            RestClient.Instance
                .AddResponseConvertor(new RestSharpResponseConverter())
                .SetLogger(new RestLogger(
                    m => log.Info(m),
                    m => log.Debug(m),
                    m => log.Debug(m)
                ));
        }
    }

    [TestFixture]
    public class BaseApiTest
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseApiTest));
    }
}
