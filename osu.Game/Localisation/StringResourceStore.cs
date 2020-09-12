// I decompiled my build cause git replaced my changes. For now I trapted in pains for restoring codes....
// Learn git are very important and be carefully when do any actions....
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.IO.Stores;

public class StringResourceStore : IResourceStore<string>, IDisposable
{
    private ConcurrentDictionary<string, string> stores;

    private bool isDisposed;

    /// <summary>
    /// Initializes a strings store without presets
    /// </summary>
    public StringResourceStore()
    {
        stores = new ConcurrentDictionary<string, string>();
    }

    /// <summary>
    /// Initializes a strings store with arrays (each array should have two strings, no more or less)
    /// </summary>
    public StringResourceStore(params IEnumerable<string>[] arrays)
    {
        stores = new ConcurrentDictionary<string, string>();
        lock (stores)
        {
            arrays.ForEach(e =>
                stores.TryAdd(e.ElementAt(0), e.ElementAt(1)
            ));
        }
    }

    /// <summary>
    /// Initializes a strings store with prepared dictionary
    /// </summary>
    public StringResourceStore(IDictionary<string, string> dict)
    {
        stores = new ConcurrentDictionary<string, string>();
        lock (stores)
        {
            dict.ForEach(e => stores.TryAdd(e.Key, e.Value));
        }
    }

    /// <summary>
    /// Initializes a strings store with a lot string parameters
    /// </summary>
    public StringResourceStore(params Tuple<string, string>[] strings)
    {
        stores = new ConcurrentDictionary<string, string>();
        lock (stores)
        {
            strings.ForEach(e => stores.TryAdd(e.Item1, e.Item2));
        }
    }

    /// <summary>
    /// Get translated text from original text
    /// </summary>
    /// <param name="text">Original text</param>
    /// <returns>A translated text, or original if it not exist in stores</returns>
    public string Get(string text)
    {
        if (text == null)
            return null;
        if (stores.TryGetValue(text, out string result))
            return result;
        return text;
    }

    /// <summary>
    /// Get translated text from original text asynchronously
    /// </summary>
    /// <param name="text">Original text</param>
    /// <returns>Result of task</returns>
    public Task<string> GetAsync(string text)
    {
        if (text == null)
            return null;
        return Task.Run(() =>
            stores.TryGetValue(text, out var value) ? value : text
        );
    }

    /// <summary>
    /// Get all available text that supported translation.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetAvailableResources()
    {
        return stores.Keys;
    }

    /// <summary>
    /// Do not use this method. GetStream for string resource store are doesn't making any senses.
    /// </summary> 
    public Stream GetStream(string name)
    {
        throw new NotImplementedException();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            isDisposed = true;
            lock (stores)
            {
                stores.ForEach(s => stores.TryRemove(s.Key, out string _));
            }
        }
    }

    ~StringResourceStore()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
