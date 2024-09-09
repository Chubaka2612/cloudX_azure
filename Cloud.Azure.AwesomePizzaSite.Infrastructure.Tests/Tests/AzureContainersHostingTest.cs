using Cloud.Azure.AwesomePizzaSite.Infrastructure.Tests.Steps;
using CloudX.Azure.Core.ConfigurationManagement;
using CloudX.Azure.Core.Utils;
using NUnit.Framework;
using System.Linq;
using Cloud.Azure.AwesomePizzaSite.Data.Meta;


namespace Cloud.Azure.AwesomePizzaSite.Infrastructure.Tests.Tests
{
    public class AzureContainersHostingTest
    {
        private static string crName = ConfigurationManager.Configuration["ContainerRegistry"];
        private static string domain = ConfigurationManager.Configuration["Domain"];

        [Test]
        [Category(TestType.Infrustructure)]
        public void ContainerRegistryResourceTest()
        {
            var crName = ConfigurationManager.Configuration["ContainerRegistry"];
       
            var crResource = ArmSteps.GetContainerRegistryResource(crName).Get();
            VerifyThat.AssertScope(
                () => VerifyThat.AreEquals(crResource.Value.Data.LoginServer, $"{crName}.azurecr.io", "Verify login sever name for container registry is correct"),
                () => VerifyThat.AreEquals(crResource.Value.Data.PublicNetworkAccess, "Enabled", "Verify public access is 'Enabled' for container registry")
                );
        }

        [Test]
        [Category(TestType.Infrustructure)]
        public void ContainerAppResourceTest()
        {
            var caName = ConfigurationManager.Configuration["ContainerApp"];
            var ceName = ConfigurationManager.Configuration["ContainerEnv"];
         
            var caResource = ArmSteps.GetContainerAppResource(caName).Get();
            VerifyThat.AssertScope(
                () => VerifyThat.AreEquals(caResource.Value.Data.Configuration.Ingress.Fqdn, $"{caName}.{domain}", "Verify Fully Qualified Domain Name for container app is correct"),
                () => VerifyThat.AreEquals(caResource.Value.Data.Configuration.Registries.First().Server, $"{crName}.azurecr.io", "Verify registry server is correct for container app is correct"),
                () => VerifyThat.AreEquals(caResource.Value.Data.EnvironmentId, $"/subscriptions/{ConfigurationManager.SubscriptionId}/resourceGroups/{ConfigurationManager.Configuration["ResorceGroupName"]}/providers/Microsoft.App/managedEnvironments/{ceName}",
                "Verify Container App Environment is correct"),
                 () => VerifyThat.AreEquals(caResource.Value.Data.ManagedEnvironmentId, $"/subscriptions/{ConfigurationManager.SubscriptionId}/resourceGroups/{ConfigurationManager.Configuration["ResorceGroupName"]}/providers/Microsoft.App/managedEnvironments/{ceName}",
                "Verify Container App Managed Environment is correct") );
        }


        [Test]
        [Category(TestType.Infrustructure)]
        public void ContainerAppEnvironmentResourceTest()
        {
            var ceName = ConfigurationManager.Configuration["ContainerEnv"];
            var caeResource = ArmSteps.GetContainerAppEnvironmentResource(ceName).Get();

            VerifyThat.NotNull(caeResource, "Verify container app environment exists by name");
            VerifyThat.AreEquals(caeResource.Value.Data.DefaultDomain, domain, "Verify default domain for container app environment");
           
        }

    }
}
