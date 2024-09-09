using Azure.Core;
using Azure.ResourceManager.Resources;
using CloudX.Azure.Core.ConfigurationManagement;
using log4net;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Network;
using System.Linq;
using System;
using System.Collections.Generic;
using Azure.ResourceManager.ContainerRegistry;
using Azure.ResourceManager.AppContainers;
using Azure.ResourceManager;
namespace Cloud.Azure.AwesomePizzaSite.Infrastructure.Tests.Steps
{
    public static class ArmSteps
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ArmSteps));

        private static string SubscriptionId = ConfigurationManager.SubscriptionId;

        private static string ResourceGroupName = ConfigurationManager.Configuration["ResorceGroupName"];

        public static ResourceGroupResource GetResourceGroupResource()
        {
            Log.Info($"Obtain resource group resource by name {ResourceGroupName}");


            return ArmService.Instance.GetClient().GetResourceGroupResource(
                new ResourceIdentifier($"/subscriptions/{SubscriptionId}/resourceGroups/{ResourceGroupName}"));
        }

        public static VirtualMachineResource GetVirtualMachineResourceByName(string vmName) 
        {
            Log.Info($"Obtain virtual machine resource by name {vmName}");

            var virtualMachineCollection = GetResourceGroupResource().GetVirtualMachines().GetAll();
            return virtualMachineCollection.First(machine => machine.Data.Name == vmName);
        }

        public static NetworkInterfaceResource GeNetworkInterfaceResourceAssociatedWithVirtualMachineByName(this VirtualMachineResource vm, string niName)
        {
            Log.Info($"Obtaining network interface associated with virtual machine '{vm.Data.Name}' by name '{niName}'");

            var client = ArmService.Instance.GetClient();

            foreach (var nicReference in vm.Data.NetworkProfile.NetworkInterfaces)
            {
                var networkInterface = client.GetNetworkInterfaceResource(new ResourceIdentifier(nicReference.Id));
                var networkInterfaceData = networkInterface.Get().Value;

                if (networkInterfaceData.Data.Name.Equals(niName, StringComparison.OrdinalIgnoreCase))
                {
                    return networkInterfaceData;
                }
            }

            return null;
        }

        public static List<PublicIPAddressResource> GeNetworkInterfacePubliIpAddressResource(this NetworkInterfaceResource ni)
        {
            Log.Info($"Obtaining public ip of network interface '{ni.Get().Value.Data.Name}'");
            var publicIpAddressResourceList = new List<PublicIPAddressResource>();
            var publicIpConfigurations = ni.Get().Value.Data.IPConfigurations.ToList().Where(ip => ip.PublicIPAddress != null);

            foreach (var ipConfig in publicIpConfigurations)
            {
                var publicIp = ArmService.Instance.GetClient().GetPublicIPAddressResource(new ResourceIdentifier(ipConfig.PublicIPAddress.Id));
                publicIpAddressResourceList.Add(publicIp);
            }
            return publicIpAddressResourceList;
        }

        public static NetworkSecurityGroupResource GeNetworkSecurityGroupResource(this NetworkInterfaceResource ni)
        {
            Log.Info($"Obtaining network security group of network interface '{ni.Get().Value.Data.Name}'");

            return ArmService.Instance.GetClient().GetNetworkSecurityGroupResource(new ResourceIdentifier(ni.Get().Value.Data.NetworkSecurityGroup.Id));
        }


        public static ContainerRegistryResource GetContainerRegistryResource(string registryName)
        {
            Log.Info($"Obtain container registry resource by name {registryName}");

            return ArmService.Instance.GetClient()
                .GetContainerRegistryResource(
                 new ResourceIdentifier($"/subscriptions/{SubscriptionId}/resourceGroups/{ResourceGroupName}/providers/Microsoft.ContainerRegistry/registries/{registryName}"));
        }

        public static ContainerAppResource GetContainerAppResource(string containerAppName)
        {
            Log.Info($"Obtain container app resource by name {containerAppName}");

            return ArmService.Instance.GetClient()
                .GetContainerAppResource(
                 new ResourceIdentifier($"/subscriptions/{SubscriptionId}/resourceGroups/{ResourceGroupName}/providers/Microsoft.App/containerApps/{containerAppName}"));
        }


        public static ContainerAppManagedEnvironmentResource GetContainerAppEnvironmentResource(string containerAppEnv)
        {
            Log.Info($"Obtain container app environment resource by name {containerAppEnv}");

            var containerAppsEnvironmentResourceId = ContainerAppManagedEnvironmentResource.CreateResourceIdentifier(SubscriptionId, ResourceGroupName, containerAppEnv);
            return ArmService.Instance.GetClient().GetContainerAppManagedEnvironmentResource(containerAppsEnvironmentResourceId);
        }
    }
}
