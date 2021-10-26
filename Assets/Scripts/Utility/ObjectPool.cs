using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool<T> 
{
    public delegate T ObjectCreateDelegate();
    ObjectCreateDelegate _createObjectCallback;
    Action<T> _onObjectActiveCallback;
    Action<T> _onObjectDisableCallback;

    public int _size;
 
    private List<T> _freeList;
    private List<T> _usedList;
    public List<T> UsedList { get { return _usedList; } }

    public ObjectPool(int initSize, ObjectCreateDelegate createObjCallback, Action<T> onObjectActive, Action<T> onObjectDisable) {
        _size = initSize;
        _createObjectCallback = createObjCallback;
        _onObjectActiveCallback = onObjectActive;
        _onObjectDisableCallback = onObjectDisable;
        Initalize();
    }

    private T CreateObject() {
        var pooledObject = _createObjectCallback.Invoke();
        return pooledObject;
    }

    private void Initalize() {
        _freeList = new List<T>(_size);
        _usedList = new List<T>(_size);
 
        for (var i = 0; i < _size; ++i) {
            _freeList.Add(CreateObject());
        }
    }

    public T GetObject() {
        if (_freeList.Count == 0)
            _freeList.Add(CreateObject());
        
        var pooledObject = _freeList[_freeList.Count - 1];
        _freeList.RemoveAt(_freeList.Count - 1);
        _usedList.Add(pooledObject);
        _onObjectActiveCallback?.Invoke(pooledObject);

        return pooledObject;
    }
 
    public void ReturnObject(T pooledObject) {
        UnityEngine.Debug.Assert(_usedList.Contains(pooledObject));
    
        _usedList.Remove(pooledObject);
        _freeList.Add(pooledObject);
        _onObjectDisableCallback?.Invoke(pooledObject);
    }

    public void ReturnAt(int index) {
        if (index >= _usedList.Count) return;

        var obj = _usedList[index];
        _usedList.RemoveAt(index);
        _freeList.Add(obj);
        _onObjectActiveCallback?.Invoke(obj);
    }

    public void Clear() {
        for (int i = 0; i < _usedList.Count; ++i) {
            var pooledObject = _usedList[i];
            _usedList.RemoveAt(i--);
            _freeList.Add(pooledObject);
            _onObjectDisableCallback?.Invoke(pooledObject);
        }
    }
}