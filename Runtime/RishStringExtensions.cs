using System.Globalization;

namespace RishUI
{
    public static class RishStringExtensions
    {
        public static string ToLower(this RishString str) => str.IsEmpty ? str : str.value.ToLower();
        public static string ToLower(this RishString str, CultureInfo cultureInfo) => str.IsEmpty ? string.Empty : str.value.ToLower(cultureInfo);
        public static string ToLowerInvariant(this RishString str) => str.IsEmpty ? string.Empty : str.value.ToLowerInvariant();

        public static string ToUpper(this RishString str) => str.IsEmpty ? string.Empty : str.value.ToUpper();
        public static string ToUpper(this RishString str, CultureInfo cultureInfo) => str.IsEmpty ? string.Empty : str.value.ToUpper(cultureInfo);
        public static string ToUpperInvariant(this RishString str) => str.IsEmpty ? string.Empty : str.value.ToUpperInvariant();
        
        public static bool Contains(this RishString str, char value) => !str.IsEmpty && str.value.Contains(value);
        public static bool Contains(this RishString str, char value, System.StringComparison comparisonType) => !str.IsEmpty && str.value.Contains(value, comparisonType);
        public static bool Contains(this RishString str, string value) => !str.IsEmpty && str.value.Contains(value);
        public static bool Contains(this RishString str, string value, System.StringComparison comparisonType) => !str.IsEmpty && str.value.Contains(value, comparisonType);
        public static bool Contains(this RishString str, RishString value) => !str.IsEmpty && str.value.Contains(value.value);
        public static bool Contains(this RishString str, RishString value, System.StringComparison comparisonType) => !str.IsEmpty && str.value.Contains(value.value, comparisonType);
        
        public static bool StartsWith(this RishString str, char value) => !str.IsEmpty && str.value.StartsWith(value);
        public static bool StartsWith(this RishString str, string value) => !str.IsEmpty && str.value.StartsWith(value);
        public static bool StartsWith(this RishString str, string value, System.StringComparison comparisonType) => !str.IsEmpty && str.value.StartsWith(value, comparisonType);
        public static bool StartsWith(this RishString str, string value, bool ignoreCase, CultureInfo cultureInfo) => !str.IsEmpty && str.value.StartsWith(value, ignoreCase, cultureInfo);
        public static bool StartsWith(this RishString str, RishString value) => !str.IsEmpty && str.value.StartsWith(value.value);
        public static bool StartsWith(this RishString str, RishString value, System.StringComparison comparisonType) => !str.IsEmpty && str.value.StartsWith(value.value, comparisonType);
        public static bool StartsWith(this RishString str, RishString value, bool ignoreCase, CultureInfo cultureInfo) => !str.IsEmpty && str.value.StartsWith(value.value, ignoreCase, cultureInfo);
        
        public static bool EndsWith(this RishString str, char value) => !str.IsEmpty && str.value.EndsWith(value);
        public static bool EndsWith(this RishString str, string value) => !str.IsEmpty && str.value.EndsWith(value);
        public static bool EndsWith(this RishString str, string value, System.StringComparison comparisonType) => !str.IsEmpty && str.value.EndsWith(value, comparisonType);
        public static bool EndsWith(this RishString str, string value, bool ignoreCase, CultureInfo cultureInfo) => !str.IsEmpty && str.value.EndsWith(value, ignoreCase, cultureInfo);
        public static bool EndsWith(this RishString str, RishString value) => !str.IsEmpty && str.value.EndsWith(value.value);
        public static bool EndsWith(this RishString str, RishString value, System.StringComparison comparisonType) => !str.IsEmpty && str.value.EndsWith(value.value, comparisonType);
        public static bool EndsWith(this RishString str, RishString value, bool ignoreCase, CultureInfo cultureInfo) => !str.IsEmpty && str.value.EndsWith(value.value, ignoreCase, cultureInfo);
        
        public static string Replace(this RishString str, char oldChar, char newChar) => str.IsEmpty ? string.Empty : str.value.Replace(oldChar, newChar);
        public static string Replace(this RishString str, string oldValue, string newValue) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue, newValue);
        public static string Replace(this RishString str, string oldValue, string newValue, System.StringComparison comparisonType) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue, newValue, comparisonType);
        public static string Replace(this RishString str, string oldValue, string newValue, bool ignoreCase, CultureInfo cultureInfo) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue, newValue, ignoreCase, cultureInfo);
        public static string Replace(this RishString str, RishString oldValue, string newValue) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue.value, newValue);
        public static string Replace(this RishString str, RishString oldValue, string newValue, System.StringComparison comparisonType) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue.value, newValue, comparisonType);
        public static string Replace(this RishString str, RishString oldValue, string newValue, bool ignoreCase, CultureInfo cultureInfo) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue.value, newValue, ignoreCase, cultureInfo);
        public static string Replace(this RishString str, RishString oldValue, RishString newValue) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue.value, newValue.value);
        public static string Replace(this RishString str, RishString oldValue, RishString newValue, System.StringComparison comparisonType) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue.value, newValue.value, comparisonType);
        public static string Replace(this RishString str, RishString oldValue, RishString newValue, bool ignoreCase, CultureInfo cultureInfo) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue.value, newValue.value, ignoreCase, cultureInfo);
        public static string Replace(this RishString str, string oldValue, RishString newValue) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue, newValue.value);
        public static string Replace(this RishString str, string oldValue, RishString newValue, System.StringComparison comparisonType) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue, newValue.value, comparisonType);
        public static string Replace(this RishString str, string oldValue, RishString newValue, bool ignoreCase, CultureInfo cultureInfo) => str.IsEmpty ? string.Empty : str.value.Replace(oldValue, newValue.value, ignoreCase, cultureInfo);
        
        public static RishString Remove(this RishString str, int startIndex, int count)
        {
            if (str.IsEmpty) return str;
            
            str.value = str.value.Remove(startIndex, count);

            return str.value.Remove(startIndex, count);
        }
        public static RishString Remove(this RishString str, int startIndex)
        {
            if (str.IsEmpty) return str;

            return str.value.Remove(startIndex);
        }
    }
}
