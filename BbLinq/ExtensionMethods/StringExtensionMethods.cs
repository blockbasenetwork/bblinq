using System.Text.RegularExpressions;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static bool HasNonAlphanumericOrUnderscore(this string content)
        {
            var regex = new Regex("([^(A-Za-z_0-9)])");
            return regex.IsMatch(content);
        }
    }
}
