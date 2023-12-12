// Administrator
// 12

using System.Dynamic;

namespace SesameTech.Tools.Extensions;

public static class DictionaryExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static TValue TryGetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        where TValue : class
        =>
            dictionary.TryGetValue(key, out var value) ? value : null;


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static TValue TryGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        => dictionary.TryGetValue(key, out var value) ? value : default(TValue);


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static dynamic ToDynamic<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        var expandoCollection = (ICollection<KeyValuePair<string, object>>)new ExpandoObject();
        foreach (var kvp in dictionary)
        {
            expandoCollection.Add(new KeyValuePair<string, object>(kvp.Key.ToString(),kvp.Value));
        }
        return expandoCollection;
    }

}