using Cloud.Azure.AwesomePizzaSite.Infrastructure.Tests.Steps;
using CloudX.Azure.Core.ConfigurationManagement;
using CloudX.Azure.Core.Utils;
using Microsoft.Azure.Management.Network.Models;
using NUnit.Framework;
using System;
using System.Linq;
using Cloud.Azure.AwesomePizzaSite.Data.Meta;
namespace Cloud.Azure.AwesomePizzaSite.Infrastructure.Tests.Tests
{
    public class AzureDeployedResourceTest { 
  
        string vmName = $"vm-{ConfigurationManager.Configuration["SelectedBuildName"]}";
        string niName = $"vm-{ConfigurationManager.Configuration["SelectedBuildName"]}-ni";
        string nsgName = $"vm-{ConfigurationManager.Configuration["SelectedBuildName"]}-nsg";
        string ipName = $"vm-{ConfigurationManager.Configuration["SelectedBuildName"]}-ip";
        string vnetName = $"vm-{ConfigurationManager.Configuration["SelectedBuildName"]}-vnet";
        string publicIpAddress = ConfigurationManager.Configuration["PublicIp"];

        [Test]
        [Category(TestType.Infrustructure)]
        public void VirtualMachineResourceTest()
        {
            var expectedOS = "Linux";
            var expectedHardwareSize = "Standard_B1s";
       
            var vmResource = ArmSteps.GetVirtualMachineResourceByName(vmName);
            var niResource = vmResource.GeNetworkInterfaceResourceAssociatedWithVirtualMachineByName(niName);
            VerifyThat.NotNull(vmResource, "Verify virtuam machine resource is not null");

            VerifyThat.AssertScope(
                () => VerifyThat.AreEquals(vmResource.Data.StorageProfile.OSDisk?.OSType.ToString(), expectedOS, "Verify vm has expected OS"),
                () => VerifyThat.AreEquals(vmResource.Data.HardwareProfile?.VmSize.ToString(), expectedHardwareSize, "Verify vm has expected hardware size"),
                () => VerifyThat.AreEquals(niResource.GeNetworkInterfacePubliIpAddressResource().Count(), 1, "Verify vm has only 1 public ip address"),
                () => VerifyThat.AreEquals(niResource.GeNetworkInterfacePubliIpAddressResource().First().Get().Value.Data.IPAddress, publicIpAddress, "Verify vm has correct public ip address")
                );
        }


        [Test]
        [Category(TestType.Infrustructure)]
        public void PublicIpAddressResourceTest()
        {
            var vmResource = ArmSteps.GetVirtualMachineResourceByName(vmName);
            var niResource = vmResource.GeNetworkInterfaceResourceAssociatedWithVirtualMachineByName(niName);

            VerifyThat.NotNull(niResource, "Verify vm has associated network intarface");
            VerifyThat.AssertScope(
                () => VerifyThat.AreEquals(niResource.GeNetworkInterfacePubliIpAddressResource().Count(), 1, "Verify public ip address assocciated with correct network interface"),
                () => VerifyThat.AreEquals(niResource.GeNetworkInterfacePubliIpAddressResource().First().Get().Value.Data.IPAddress, publicIpAddress, "Verify public Ip address resource has correct value"),
                () => VerifyThat.AreEquals(niResource.GeNetworkInterfacePubliIpAddressResource().First().Get().Value.Data.Name, ipName, "Verify public Ip address resource has correct name")
                );
        }

        [Test]
        [Category(TestType.Infrustructure)]
        public void NetworkInterfaceResourceTest()
        {
            var vmResource = ArmSteps.GetVirtualMachineResourceByName(vmName);
            var niResource = vmResource.GeNetworkInterfaceResourceAssociatedWithVirtualMachineByName(niName);

            VerifyThat.NotNull(niResource, "Verify network interface attached to virtual machine");
            VerifyThat.AssertScope(
                () => VerifyThat.AreEquals(niResource.GeNetworkInterfacePubliIpAddressResource().First().Get().Value.Data.IPAddress, publicIpAddress, "Verify network interface has correct public ip address"),
                () => VerifyThat.IsTrue(niResource.Get().Value.Data.NetworkSecurityGroup.Id.ToString().Contains(nsgName, StringComparison.OrdinalIgnoreCase), "Verify network interface attached to correct network security group")
                );
        }

        [Test]
        [Category(TestType.Infrustructure)]
        public void NetworkSecurityGroupResourceTest()
        {
            var vmResource = ArmSteps.GetVirtualMachineResourceByName(vmName);
            var niResource = vmResource.GeNetworkInterfaceResourceAssociatedWithVirtualMachineByName(niName);
            var nsqResource = niResource.GeNetworkSecurityGroupResource();


            VerifyThat.NotNull(nsqResource, "Verify network security group attached to virtual machine");

            //Check inbound security rules for SSH accessibility (port 22)
            bool sshAccessible = false;
            foreach (var rule in nsqResource.Get().Value.Data.SecurityRules)
            {
                // Check if the rule allows inbound SSH access
                if (rule.Access == SecurityRuleAccess.Allow &&
                    rule.Direction == SecurityRuleDirection.Inbound &&
                    rule.DestinationPortRange == "22" &&
                    (rule.Protocol == SecurityRuleProtocol.Tcp || rule.Protocol == SecurityRuleProtocol.Asterisk))
                {

                    sshAccessible = true;
                }
            }
            VerifyThat.IsTrue(sshAccessible, "Verify vm is accessible from the internet SHH (port 22)");

            bool httpAccessible = false;
            foreach (var rule in nsqResource.Get().Value.Data.SecurityRules)
            {
                // Check if the rule allows inbound HTTP access on port 3000
                if (rule.Access == SecurityRuleAccess.Allow &&
                    rule.Direction == SecurityRuleDirection.Inbound &&
                    rule.DestinationPortRange == "3000" &&
                    (rule.Protocol == SecurityRuleProtocol.Tcp || rule.Protocol == SecurityRuleProtocol.Asterisk))
                {

                    httpAccessible = true;
                }
            }
            VerifyThat.IsTrue(httpAccessible, "Verify vm is accessible is accessible from the internet HTTP (port 3000)");
        }

        [Test]
        [Category(TestType.Infrustructure)]
        public void VirtualNetworkResourceTest()
        {
            var vmResource = ArmSteps.GetVirtualMachineResourceByName(vmName);
            var niResource = vmResource.GeNetworkInterfaceResourceAssociatedWithVirtualMachineByName(niName);
            var vnetworkPresent = niResource.Get().Value.Data.IPConfigurations.ToList().Any(ipConfig => ipConfig.Subnet.Id.ToString().Contains($"/virtualNetworks/{vnetName}/", StringComparison.OrdinalIgnoreCase));

            VerifyThat.IsTrue(vnetworkPresent, "Verify virtual network is associated with virtual machine");
        }
    }
}
