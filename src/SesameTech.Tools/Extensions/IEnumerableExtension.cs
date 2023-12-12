using System.Diagnostics;

namespace SesameTech.Tools.Extensions;

public static class IEnumerableExtension
{

    /// <summary>
    /// 遍历IEnumerable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="action">回调方法</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var s in source)
        {
            action(s);
        }
    }

    /// <summary>
    /// 异步foreach
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="func"></param>
    /// <param name="maxParallelCount">最大并行数</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> func, int maxParallelCount,
        CancellationToken cancellationToken = default)
    {
        if (Debugger.IsAttached)
        {
            foreach (var item in source)
            {
                await func(item);
            }
            return;
        }

        var list = new List<Task>();
        foreach (var item in source)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            list.Add(func(item));
            if (list.Count <= maxParallelCount) continue;
            await Task.WhenAll(list);
            list.RemoveAll(t => t.IsCompleted);
        }

        await Task.WhenAll(list);
    }
}