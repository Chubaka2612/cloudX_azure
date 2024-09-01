using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CloudX.Azure.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string stringToTransform)
        {
            if (stringToTransform == null)
            {
                throw new ArgumentNullException(nameof(stringToTransform));
            }

            var splittedStrings = Regex.Split(stringToTransform.ToString(), @"(?<!^)(?=[A-Z])");
            return string.Join(" ", splittedStrings);
        }

        public static string ToFirstUpperCase(this string stringToTransform)
        {
            if (stringToTransform == null)
            {
                throw new ArgumentNullException(nameof(stringToTransform));
            }
            var splittedByCamel = stringToTransform.SplitCamelCase();
            var lowerCase = splittedByCamel.ToLower();
            return lowerCase.First().ToString().ToUpper() + lowerCase.Substring(1);
        }

        public static string ToTitleCase(this string stringToTransform)
        {
            if (stringToTransform == null)
            {
                throw new ArgumentNullException(nameof(stringToTransform));
            }

            var textInfo = CultureInfo.InvariantCulture.TextInfo;
            return textInfo.ToTitleCase(stringToTransform);
        }

        public static string ToCamelCase(this string stringToTransform)
        {
            if (stringToTransform == null)
            {
                throw new ArgumentNullException(nameof(stringToTransform));
            }
            return stringToTransform.ToTitleCase().Replace(" ", string.Empty);
        }

        public static string RemoveWhitespace(this string stringToTransform)
        {
            if (stringToTransform == null)
            {
                throw new ArgumentNullException(nameof(stringToTransform));
            }
            return stringToTransform.Replace(" ", string.Empty);
        }

    }
}
