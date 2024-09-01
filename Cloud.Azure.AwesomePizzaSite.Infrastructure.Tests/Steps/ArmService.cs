using Azure.Identity;
using Azure.ResourceManager;
using CloudX.Azure.Core.ConfigurationManagement;

namespace Cloud.Azure.AwesomePizzaSite.Infrastructure.Tests.Steps
{
    internal class ArmService
    {
        private static ArmService _instance;

        private readonly ArmClient _armService;

        private static readonly object Padlock = new object();

        private ArmService()
        {
            var subscriptionId = ConfigurationManager.SubscriptionId;
            _armService = new ArmClient(new DefaultAzureCredential(), subscriptionId);
        }

        public static ArmService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ArmService();
                        }
                    }
                }
                return _instance;
            }
        }

        public ArmClient GetClient()
        {
            return _armService;
        }
    }
}
