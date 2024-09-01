using System.Reflection;

namespace CloudX.Azure.Core.Extensions
{
    public static class MemberInfoExtension
    {
        public static Type GetMemberType(this MemberInfo memberInformation)
        {
            if (memberInformation == null)
            {
                throw new ArgumentNullException(nameof(memberInformation));
            }

            return memberInformation.MemberType switch
            {
                MemberTypes.Field => ((FieldInfo)memberInformation).FieldType,
                MemberTypes.Property => ((PropertyInfo)memberInformation).PropertyType,
                _ => throw new ArgumentOutOfRangeException(nameof(memberInformation), "This extension method can only work with Fields or Properties"),
            };
        }

        public static object GetMemberValue(this MemberInfo memberInformation, object instance)
        {
            if (memberInformation == null)
            {
                throw new ArgumentNullException(nameof(memberInformation));
            }

            return memberInformation.MemberType switch
            {
                MemberTypes.Field => ((FieldInfo)memberInformation).GetValue(instance),
                MemberTypes.Property => ((PropertyInfo)memberInformation).GetValue(instance),
                _ => throw new ArgumentOutOfRangeException(nameof(memberInformation), "This extension method can only work with Fields or Properties"),
            };
        }

        public static void SetMemberValue(this MemberInfo memberInfo, object instance, object value)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }

            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)memberInfo).SetValue(instance, value);
                    break;
                case MemberTypes.Property:
                    {
                        var propertyInfo = (PropertyInfo)memberInfo;
                        if (propertyInfo.SetMethod != null)
                        {
                            propertyInfo.SetValue(instance, value);
                        }
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(memberInfo), "This extension method can only work with Fields and Properties");
            }
        }

        public static bool HasType<T>(this MemberInfo member)
        {
            return typeof(T).IsAssignableFrom(member.GetMemberType());
        }
    }
}
