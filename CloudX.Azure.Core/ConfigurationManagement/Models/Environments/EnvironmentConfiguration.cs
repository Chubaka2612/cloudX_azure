using CloudX.Azure.Core.Enums;

namespace CloudX.Azure.Core.ConfigurationManagement.Models.Environments
{
    public class EnvironmentConfiguration
    {
        public EnvironmentConfiguration()
        {
            EnvironmentOptions = new List<EnvironmentOptions>();
        }

        public EnvironmentType SelectedEnvironment { get; set; }

        public IList<EnvironmentOptions> EnvironmentOptions { get; }
    }
}
