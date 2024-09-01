using CloudX.Azure.Core.Enums;

namespace CloudX.Azure.Core.ConfigurationManagement.Models.Environments
{
    public class EnvironmentOptions
    {
        public EnvironmentType EnvironmentName { get; set; }

        public Uri BaseWebUrl { get; set; }

        public Uri BaseApiUrl { get; set; }

    }
}
