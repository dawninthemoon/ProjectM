using UnityEngine;

namespace Utills
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                    Debug.LogError(typeof(T) + " doesn't find");
                }

                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = GetComponent<T>();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            OnAwake();
        }

        protected abstract void OnAwake();
    }
}