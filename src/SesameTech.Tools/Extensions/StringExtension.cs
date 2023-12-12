// Administrator
// 12

using System.Text;
using System.Text.RegularExpressions;

namespace SesameTech.Tools.Extensions;

public static class StringExtension
{
    /// <summary>
    /// 如果这个字符串为null，返回一个空得字符串，
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Safe(this string str)
    {
        return str ?? string.Empty;
    }

    /// <summary>
    /// 移除一个子串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="valueToRemove"></param>
    /// <returns></returns>
    public static string Without(this string str, string valueToRemove) => str.Replace(valueToRemove, string.Empty);

    /// <summary>
    /// 移除所有非A-Za-z得字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToAlpha(this string str)
        => Regex.Replace(str, "[^A-Za-z]", string.Empty);

    /// <summary>
    /// 移除所有非数字得字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToNumeric(this string str)
        => Regex.Replace(str, "[^0-9]", string.Empty);

    /// <summary>
    /// 移除所有非A-Za-z、数字得字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToAlphaNumeric(this string str)
        => Regex.Replace(str, "[^A-Za-z0-9]", string.Empty);

    public static string ToNullIfEmpty(this string str)
    {
        if (null == str)
        {
            return null;
        }
        return string.IsNullOrEmpty(str.Trim()) ? null : str;
    }

    public static string StripLeadingZeros(this string str)
        => str.TrimStart(new[] { '0' });

    public static string StripTrailingZeros(this string str)
        => str.TrimEnd(new[] { '0' });

    public static string FromCamelCase(this string str)
        => Regex.Replace(str, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

    public static bool IsAlpha(this string str)
        => str.All(char.IsLetter);

    public static bool IsNumeric(this string str)
        => str.All(char.IsDigit);
    /// <summary>
    /// Determines if the string contains only alpha-numeric characters
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsAlphaNumeric(this string str)
        => str.All(char.IsLetterOrDigit);

    /// <summary>
    /// Determines if the string contains only uppercase characters
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsUpper(this string str)
    => str.All(char.IsUpper);

    /// <summary>
    /// Determines if the string contains only lowercase characters
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsLower(this string str)
    => str.All(char.IsLower);

    /// <summary>
    /// Lets you do a String.Contains() while using StringComparison
    /// </summary>
    /// <param name="str"></param>
    /// <param name="stringToFind"></param>
    /// <param name="stringComparison"></param>
    /// <returns></returns>
    public static bool Contains(this string str, string stringToFind, StringComparison stringComparison)
        => str.IndexOf(stringToFind, stringComparison) >= 0;

    /// <summary>
    /// Given a delimited string, this will split it and return the values cleanly in a List
    /// </summary>
    /// <param name="str"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static List<string> DelimitedToList(this string str, char delimiter = ',')
        => str.Safe().Split(delimiter)
            .Where(x => !string.IsNullOrEmpty(str))
            .Select(x => x.Trim()).ToList();

    /// <summary>
    /// iven a delimited string, this will split it and return the values cleanly in a Dictionary
    /// </summary>
    /// <param name="str"></param>
    /// <param name="outerDelimiter"></param>
    /// <param name="innerDelimiter"></param>
    /// <returns></returns>
    public static Dictionary<string, string?> DelimitedToDictionary(this string str, char outerDelimiter = '|',
        char innerDelimiter = ',')
        => str.Safe().Split(outerDelimiter).Where(s => !string.IsNullOrEmpty(s))
            .ToDictionary(s => s.DelimitedToList(innerDelimiter)[0],
                s => s.Contains(innerDelimiter) ? s.DelimitedToList(innerDelimiter)[0] : null);

    /// <summary>
    /// Truncates a string to the maxlength, or the length or the string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string Truncate(this string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return str.Substring(0, Math.Min(str.Length, maxLength));
    }

    /// <summary>
    /// Removes the last occurrence of a substring from a string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="valueToRemove"></param>
    /// <returns></returns>
    public static string RemoveLastOccurrenceOf(this string str, string valueToRemove)
    {
        if (null == str)
        {
            return null;
        }
        int lastIndexOf = str.LastIndexOf(valueToRemove, StringComparison.Ordinal);
        return str.Remove(lastIndexOf, valueToRemove.Length);
    }

    /// <summary>
    /// Throws a <see cref="Exception"/> if the given string is null or empty
    /// </summary>
    /// <param name="str"></param>
    /// <param name="objectName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string ThrowIfNullOrEmpty(this string str, string objectName, string message = null)
    {
        if (string.IsNullOrEmpty(str))
        {
            var msg = $"{objectName} cannot be null";

            if (null != message)
            {
                msg = $". {message}";
            }

            throw new Exception(msg);
        }
        return str;
    }

    /// <summary>
    /// Splits this string and will upper the first character of each element. It will then join the elements back as specified (by default, it joins them without any whitespace)
    /// </summary>
    /// <param name="str">The string to format</param>
    /// <param name="splitBy">The character that should be used to split the string into individual elements</param>
    /// <param name="joinWith">The string that should be used to join the elements back together, which is an empty string by default</param>
    /// <returns></returns>
    public static string ToCamelCase(this string str, char splitBy = '_', string joinWith = "")
    {
        if (null == str)
        {
            return null;
        }

        var returnString = string.Empty;

        try
        {
            foreach (var substring in str.Split(splitBy))
            {
                returnString += ToUpperFirstLetter(substring) + joinWith;
            }
        }
        catch (Exception e)
        {
            return str;
        }

        return string.Empty == joinWith
            ? returnString
            : returnString.Substring(0, returnString.LastIndexOf(joinWith, StringComparison.Ordinal));
    }

    /// <summary>
    /// Uppers the first character, lowers all subsequent characters
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static string ToUpperFirstLetter(this string str)
    {
        if (null == str)
        {
            return null;
        }

        if (str.Length > 1)
        {
            return $"{char.ToUpper(str[0])}{str.Substring(1).ToLower()}";
                //char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        return str.ToUpper();
    }

    /// <summary>
    /// Takes this string in the singular tense and will make it plural if the count of the items it's associated to is greater than 1
    /// </summary>
    /// <param name="str">The string to potentially make plural</param>
    /// <param name="itemCount">The count of the items that dictates whether or not the string should be plural</param>
    /// <param name="appropriateSuffix">The string that should be used when making the word plural</param>
    /// <returns></returns>
    public static string ToPluralIfNeeded(this string str, int itemCount, string appropriateSuffix = "s")
        => itemCount > 1
            ? $"{str}{appropriateSuffix}"
            : str;

    /// <summary>
    /// Returns a substring starting the first index of the removeAfter parameter, ending at the end of the string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="removeAfter"></param>
    /// <param name="includeRemoveAfterString"></param>
    /// <returns></returns>
    public static string SubstringBefore(this string str, string removeAfter, bool includeRemoveAfterString = false)
    {
        if (null == str)
        {
            return null;
        }

        try
        {
            return str.Substring(0,
                str.IndexOf(removeAfter, StringComparison.Ordinal) + (includeRemoveAfterString ? 1 : 0));
        }
        catch 
        {

            return str;
        }
    }

    /// <summary>
    /// Returns a substring starting the 0th position, ending at the removeAfter parameter
    /// </summary>
    /// <param name="str"></param>
    /// <param name="removeBefore"></param>
    /// <param name="includeRemoveBeforeString"></param>
    /// <returns></returns>
    public static string SubstringAfter(this string str, string removeBefore, bool includeRemoveBeforeString = false)
    {
        if (null == str)
        {
            return null;
        }

        if (null == removeBefore || !str.Contains(removeBefore))
        {
            return str;
        }

        try
        {
            return str.Substring(
                str.IndexOf(removeBefore, StringComparison.Ordinal) + (includeRemoveBeforeString ? 0 : 1));
        }
        catch
        {
            return str;
        }
    }

    /// <summary>
    /// Returns a substring between two other substrings
    /// </summary>
    /// <param name="str"></param>
    /// <param name="firstSubstringToFind"></param>
    /// <param name="lastSubstringToFind"></param>
    /// <returns></returns>
    public static string SubstringBetween(this string str, string firstSubstringToFind, string lastSubstringToFind)
    {
        if (null == str)
        {
            return null;
        }

        var endingIndex = str.LastIndexOf(lastSubstringToFind, StringComparison.Ordinal);
        if (endingIndex > 0 && endingIndex < str.Length)
        {
            str = str.Remove(endingIndex);
        }

        int startingIndex = str.IndexOf(firstSubstringToFind, StringComparison.Ordinal);

        if (startingIndex > -1)
        {
            str = str.Substring(startingIndex + firstSubstringToFind.Length);
        }

        return str;
    }

    /// <summary>
    /// Removes a specified number of characters from the end of a string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="characterCountToRemove"></param>
    /// <returns></returns>
    public static string RemoveCharactersFromEnd(this string str, int characterCountToRemove)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        try
        {
            return str.Substring(0, str.Length - characterCountToRemove);
        }
        catch (Exception )
        {
            return str;
        }
    }

    /// <summary>
    /// This is used when casting our enums to strings. There is always an Unknown value in the 0th position, which should usually be resolved to null
    /// </summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ResolveUnknown(this string str)
        => string.IsNullOrEmpty(str) || "unknown" == str.ToLower()
            ? null
            : str;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputString"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string SafeSubstring(this string inputString, int startIndex, int length = -1)
    {
        if (null == inputString)
        {
            return null;
        }

        int inputLength = inputString.Length;
        var adjustedStartIndex = (startIndex >= 0) ? startIndex : 0;

        if (adjustedStartIndex >= inputLength)
        {
            return null;
        }

        if (length < 0 || (adjustedStartIndex + length) > inputLength)
        {
            return inputString.Substring(adjustedStartIndex);
        }

        return inputString.Substring(adjustedStartIndex, length);
    }

    public static string RemoveNonAlphaNumericCharacters(this string inputString, params char[] excludeCharacters)
    {
        if (null == inputString)
        {
            return null;
        }

        var outputString = new StringBuilder();
        var hasExcludeCharacters = (excludeCharacters != null && excludeCharacters.Any());
        var stringLength = inputString.Length;

        for (int i = 0; i < stringLength; i++)
        {
            var character = inputString[i];
            if (char.IsLetterOrDigit(character) || (hasExcludeCharacters && excludeCharacters.Contains(character)))
            {
                outputString.Append(character);
            }
        }

        return outputString.ToString();
    }

    public static string RemoveNonAlphaCharacters(this string inputString, params char[] excludeCharacters)
    {
        if (null == inputString)
        {
            return null;
        }

        var outputString = new StringBuilder();
        var hasExcludeCharacters = (excludeCharacters != null && excludeCharacters.Any());
        var stringLength = inputString.Length;

        for (var i = 0; i < stringLength; i++)
        {
            var character = inputString[i];
            if (char.IsLetter(character) || (hasExcludeCharacters && excludeCharacters!.Contains(character)))
            {
                outputString.Append(character);
            }
        }

        return outputString.ToString();
    }
}