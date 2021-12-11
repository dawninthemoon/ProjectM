using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIItemPool
{
    private readonly GameObject _prefab;
    private readonly int _poolSize;
    private readonly Transform _parent;

    private readonly Queue<GameObject> _pool = new Queue<GameObject>();

    public readonly Dictionary<int, GameObject> Items = new Dictionary<int, GameObject>();

    public UIItemPool(GameObject prefab, Transform parent, int poolSize)
    {
        if (_parent)
            Debug.LogError($"prefab is null!!!");
        _prefab = prefab;
        _poolSize = poolSize;
        _parent = parent;
    }

    public T Add<T>()
        where T : UIPresenter
    {
        return Add<T>(Items.Count);
    }

    public void Remove(GameObject item)
    {
        Remove(IndexOf(item));
    }

    public int IndexOf(GameObject item)
    {
        foreach (var pair in Items)
            if (pair.Value == item)
                return pair.Key;

        return -1;
    }

    public T Add<T>(int key)
        where T : UIPresenter
    {
        var item = GetFromPool<T>();
        Items.Add(key, item.gameObject);
        return item;
    }

    public void Remove(int key)
    {
        var item = Items[key];
        Items.Remove(key);
        ReleaseToPool(item);
    }

    public void Clear()
    {
        foreach (var item in Items.Values) ReleaseToPool(item);

        Items.Clear();
    }

    private T GetFromPool<T>()
        where T : UIPresenter
    {
        if (_pool.Count <= 0)
            for (var i = 0; i < _poolSize; i++)
            {
                var newObj = Object.Instantiate(_prefab, _parent, false);
                newObj.gameObject.SetActive(false);
                _pool.Enqueue(newObj);
            }

        var obj = _pool.Dequeue();
        obj.transform.SetParent(_parent, false);
        obj.gameObject.SetActive(true);
        obj.transform.SetSiblingIndex(Items.Count);
        return obj.AddComponent<T>();
    }

    //IsOpen이 false될 떄 불러주는 것도 나쁘지 않을 듯
    private void ReleaseToPool(GameObject item)
    {
        item.gameObject.SetActive(false);
        _pool.Enqueue(item);

        var presenter = item.GetComponentInChildren<IDisposable>();
        presenter?.Dispose();
    }
}