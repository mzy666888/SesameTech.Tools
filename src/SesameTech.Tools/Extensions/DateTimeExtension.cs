// Administrator
// 12

using System.Reflection.Metadata;
using System.Text;

namespace SesameTech.Tools.Extensions;

public static class DateTimeExtension
{
    public static string ToSafeDateString(this DateTime? dateTime)
    {
        if (null == dateTime)
        {
            return string.Empty;
        }

        return dateTime.Value.ToShortDateString();
    }

    public static bool IsWeekDay(this DateTime date)
        => date.DayOfWeek != DayOfWeek.Saturday && DayOfWeek.Sunday != date.DayOfWeek;

    public static DateTime AddBusinessDays(this DateTime datetime, int businessDaysToAdd)
    {
        var completeWeeks = businessDaysToAdd / 5;
        var date = datetime.AddDays(completeWeeks * 7);
        businessDaysToAdd = businessDaysToAdd % 5;

        for (int i = 0; i < businessDaysToAdd; i++)
        {
            date = date.AddDays(1);
            while (!IsWeekDay(date))
            {
                date= date.AddDays(1);
            }
        }

        return date;
    }

    public static DateTime ToEasternStandardTime(this DateTime dateTime)
    {
        var dateTimeAsUtc = dateTime.ToUniversalTime();
        TimeZoneInfo easternZone;
        try
        {
            easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");//PC
        }
        catch (Exception )
        {
            easternZone = TimeZoneInfo.FindSystemTimeZoneById("US/Eastern"); //Mac
        }

        return TimeZoneInfo.ConvertTimeFromUtc(dateTimeAsUtc, easternZone);
    }

    public static string ToPrintFriendly(this TimeSpan timeSpan, bool displaySeconds = true,
        bool displayMilliseconds = false)
    {
        var stringBuilder = new StringBuilder();
        if (timeSpan.Hours > 0)
        {
            stringBuilder.AppendFormat($"{timeSpan.Hours} hr.");
        }

        if (timeSpan.Hours > 0 || timeSpan.Minutes > 0)
        {
            stringBuilder.AppendFormat($"{timeSpan.Minutes} min.");
        }

        if (displaySeconds)
        {
            stringBuilder.AppendFormat($"{timeSpan.Seconds:D2} sec.");
        }

        if (displayMilliseconds)
        {
            stringBuilder.AppendFormat($"{timeSpan.Milliseconds:D2}");
        }

        return stringBuilder.ToString();

    }

    /// <summary>
    /// Throws an <see cref="Exception"/> if the given DateTime is set to the default (min) value
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="objectName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static DateTime ThrowIfNullOrEmpty(this DateTime dateTime, string objectName)
    {
        if (DateTime.MinValue == dateTime)
        {
            throw new Exception($"{objectName} was DateTime.MinValue");
        }

        return dateTime;
    }
}