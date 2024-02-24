// Administrator
// 12

using System.ComponentModel;
using System.Data;

namespace SesameTech.Tools.Extensions;

public static class ListExtension
{
    public static IEnumerable<TSource> DictionaryBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();
        foreach (var item in source)
        {
            if (seenKeys.Add(keySelector(item)))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> Flatten<T>(this IEnumerable<T> enumerable, Func<T, IEnumerable<T>> propertySelector)
    {
        if (null == enumerable)
        {
            return Enumerable.Empty<T>();
        }

        var enumerableCopy = enumerable as T[] ?? enumerable.ToArray();
        return enumerableCopy.SelectMany(c => propertySelector(c).Flatten(propertySelector)).Concat(enumerableCopy);
    }

    public static List<T> Move<T>(this List<T> list, int oldIndex, int newIndex)
    {
        if (oldIndex == newIndex)
        {
            return list;
        }

        var aux = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, aux);
        return list;
    }

    public static DataTable ToDataTable<T>(this List<T> data)
    {
        var props = TypeDescriptor.GetProperties(typeof(T));

        var table = new DataTable();

        for (var i = 0; i < props.Count; i++)
        {
            var prop= props[i];
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
            }
            else
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
        }

        var values = new object[props.Count];

        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }

            table.Rows.Add(values);

        }

        return table;
    }

    public static List<T> RemoveNulls<T>(this List<T> list) where T : class
    {
        list.RemoveAll(obj => null == obj);

        return list;
    }
}