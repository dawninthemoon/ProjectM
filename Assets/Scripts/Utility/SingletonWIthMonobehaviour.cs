using UnityEngine;

public class SingletonWithMonoBehaviour<T> : MonoBehaviour where T : Component
{
    protected static T _instance;

    public static T GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<T>();
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                _instance = obj.AddComponent<T>();
            }
        }
        return _instance;
    }

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (_instance && _instance != this) return;

        _instance = this as T;
    }
}