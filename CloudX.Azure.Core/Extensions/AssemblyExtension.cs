using System.Reflection;
using System.Text;

namespace CloudX.Azure.Core.Extensions
{
    public static class AssemblyExtension
    {
        public static string GetManifestedResourceContent(this Assembly assembly, string resourceName)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var manifestedResourceStream = assembly.GetManifestResourceStream(resourceName);
            if (manifestedResourceStream == null)
            {
                throw new FileNotFoundException($"Resource with name '{resourceName}' is not found.");
            }
            using (var streamReader = new StreamReader(manifestedResourceStream, Encoding.UTF8))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
