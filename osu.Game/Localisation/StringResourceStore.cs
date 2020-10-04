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

/// <summary>
/// This class used for implementing framework text injection and dropmenus text injection support.
/// It should be initialized and injected to Localisation property when game loading.
/// </summary>
public class StringResourceStore : IResourceStore<string>, IDisposable
{
    private StringResourceStore[] baseStores;
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
    /// otherwize it will throws exception.
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

    public StringResourceStore(params StringResourceStore[] stores)
    {
        this.stores = new ConcurrentDictionary<string, string>();
        baseStores = stores;
    }

    /// <summary>
    /// Get translated text from original text
    /// </summary>
    /// <param name="text">Original text</param>
    /// <returns>A translated text, or original text if it not exist in stores</returns>
    public string Get(string text)
    {
        if (text == null)
            return null;
        if (stores.TryGetValue(text, out string result))
            return result;
        if (baseStores != null)
            return getFromBases(text);

        return text;
    }

    /// <summary>
    /// Get translated text from original text asynchronously
    /// </summary>
    /// <param name="text">Original text</param>
    /// <returns>Result of task that contains translated text</returns>
    public Task<string> GetAsync(string text)
    {
        if (text == null)
            return null;
        return Task.Run(() =>
        {
            if (stores.TryGetValue(text, out string result))
                return result;
            if (baseStores != null)
                return getFromBases(text);
            return text;
        });
    }

    /// <summary>
    /// Get all available text that supported translation.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetAvailableResources()
    {
        List<string> result = new List<string>(stores.Keys);
        if (baseStores != null)
            baseStores.ForEach(i => result.AddRange(i.GetAvailableResources()));
        return result;
    }

    private string getFromBases(string text)
    {
        string result = text;
        baseStores.ForEach(i => { if (result == text) result = i.Get(result); });
        return result;
    }

    /// <summary>
    /// Do not use this method. GetStream for string resource store are doesn't making any senses.
    /// </summary> 
    public Stream GetStream(string name)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Cleanup store and dispose this object.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            isDisposed = true;
            lock (stores)
            {
                stores.ForEach(s => stores.TryRemove(s.Key, out string _));
            }
            stores.Clear();
            stores = null;
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
