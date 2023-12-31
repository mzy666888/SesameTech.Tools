﻿// Administrator
// 12

namespace SesameTech.Tools.Extensions;

public static class EnumExtension
{
    /// <summary>
    /// Gets an attribute on an enum field value
    /// </summary>
    /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
    /// <param name="enumVal">The enum value</param>
    /// <returns>The attribute of type T that exists on the enum value</returns>
    public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
    {
        var type = enumVal.GetType();
        var meminfo = type.GetMember(enumVal.ToString());
        var attributes = meminfo[0].GetCustomAttributes(typeof(T), false);

        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }
}