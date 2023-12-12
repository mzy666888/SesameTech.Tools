// Administrator
// 12

namespace SesameTech.Tools.Extensions;

public static class ArrayExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static T[] AsSafe<T>(this T[] array) where T : new()
        => array ?? new[] { new T() };
}