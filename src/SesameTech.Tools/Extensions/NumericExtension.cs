// Administrator
// 12

namespace SesameTech.Tools.Extensions;

public static class NumericExtension
{
    public static string ToSafeString(this int? val, string format = null)
        => val.HasValue ? GetFormattedValue(val.Value, format) : string.Empty;

    public static string ToSafeString(this double? val, string format = null)
        => val.HasValue ? GetFormattedValue(val.Value, format):string.Empty;

    public static string ToSafeString(this decimal? val, string format = null)
        => val.HasValue ? GetFormattedValue(val.Value, format) : string.Empty;

    public static bool? ToNullableBool(this int? val)
        => val.HasValue ? Convert.ToBoolean(val.Value) : (bool?)null;

    public static Int16 ToInt16(this int val)
        => Convert.ToInt16(val);

    private static string GetFormattedValue(int val, string format)
        => null == format ? val.ToString() : val.ToString(format);

    private static string GetFormattedValue(double val, string format)
        => null == format ? val.ToString() : val.ToString(format);

    private static string GetFormattedValue(decimal val, string format)
        => null == format ? val.ToString() : val.ToString(format);
}