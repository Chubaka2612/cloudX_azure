using CloudX.Azure.Core.Enums;

namespace CloudX.Azure.Core.ConfigurationManagement.Models.Drivers
{
    public class DriverOptions
    {
        public DriverOptions()
        {
            Options = new Dictionary<string, string>();
        }

        public DriverType DriverKey { get; set; }

        public string DriverName { get; set; }

        public string DriverVersion { get; set; }

        public Dictionary<string, string> Options { get; }
    }
}
