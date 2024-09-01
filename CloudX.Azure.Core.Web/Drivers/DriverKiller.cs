using System.Diagnostics;


namespace CloudX.Azure.Core.Web.Drivers
{
    public class DriverKiller
    {
        public string[] ProcessToKill { get; set; } =
        {
            "chromedriver",
        };

        public void KillRunningDrivers()
        {
            foreach (var process in ProcessToKill)
            {
                Process.GetProcessesByName(process).ToList()
                    .ForEach(p => KillProcess(p));
            }
        }

        private void KillProcess(Process process)
        {
            try
            {
                if (!process.HasExited)
                {
                    process.Kill();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}