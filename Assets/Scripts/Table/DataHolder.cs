using System;
using System.Collections.Generic;

public sealed class DataHolder<T>
    where T : class
{
    public string Filename { get; private set; }

    private T[] all;
    private Dictionary<string, T> dictionary;

    public static DataHolder<T> Create(T[] data, Func<T, string> idGetter)
    {
        DataHolder<T> holder = new DataHolder<T>();
        holder.Set(data, idGetter);

        return holder;
    }

    public static DataHolder<T> Create(string fileName, Func<T, string> idGetter)
    {
        var data = ClientDataLoader.Load<T>(fileName);
        var holder = Create(data, idGetter); ;
        holder.Filename = fileName;
        return holder;
    }

    public T this[int id]
    {
        get
        {
            T value;
            dictionary.TryGetValue(id.ToString(), out value);
            return value;
        }
    }

    public T this[long id]
    {
        get
        {
            T value;
            dictionary.TryGetValue(id.ToString(), out value);
            return value;
        }
    }

    public T this[string id]
    {
        get
        {
            T value;
            dictionary.TryGetValue(id, out value);
            return value;
        }
    }

    public T[] All
    {
        get { return all; }
    }

    private DataHolder()
    {
    }

    private void Set(T[] data, Func<T, string> idGetter)
    {
        all = data;

        if (data == null || data.Length == 0)
        {
            all = new T[0];
            return;
        }

        dictionary = new Dictionary<string, T>();
        for (int i = 0; i < data.Length; ++i)
        {
            string id = idGetter(data[i]);
            if (dictionary.ContainsKey(id))
            {
                throw new ApplicationException(string.Format("Data id {0} in type {1} duplicated!", id, typeof(T).Name));
            }
            dictionary.Add(id, data[i]);
        }
    }
}