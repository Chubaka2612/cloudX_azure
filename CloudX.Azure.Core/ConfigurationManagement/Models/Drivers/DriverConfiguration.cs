using CloudX.Azure.Core.Enums;

namespace CloudX.Azure.Core.ConfigurationManagement.Models.Drivers
{
    public class DriverConfiguration
    {
        public DriverConfiguration()
        {
            DriverOptions = new List<DriverOptions>();
        }

        public DriverType SelectedDriverType { get; set; }

        public IList<DriverOptions> DriverOptions { get; set; }
    }
}
