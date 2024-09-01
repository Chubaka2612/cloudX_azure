using System.Reflection;
using System.Runtime.CompilerServices;

namespace CloudX.Azure.Core.Web.PageObjects.Extensions
{
    public static class ReflectionExtension
    {
        public static IList<MemberInfo> FilterMembers(this Type type)
        {
            var members = type.GetMembers();
            var fieldMembers = members.Where(memebr => memebr.MemberType == MemberTypes.Field && memebr.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
            var propertyMembers = members.Where(memeber => memeber.MemberType == MemberTypes.Property
                                                          && ((PropertyInfo)memeber).SetMethod != null
                                                          && memeber.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
            return fieldMembers.Concat(propertyMembers.Where(p => p.Name != "WebElement")).Where(m => m.Name != "Parent").ToList();
        }
    }
}
