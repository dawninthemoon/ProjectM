using UnityEngine;

namespace UI
{
    public class UIElem<T> where T : Object
    {
        private readonly string _tag;
        private readonly bool _optional;
        
        public T Comp { get; private set; }

        public UIElem(bool optional = false)
        {
            _optional = optional;
        }

        public UIElem(string tag, bool optional = false)
        {
            _optional = optional;
            _tag = tag;
        }

        public void Set(UITag tag)
        {
            if (typeof(T) == typeof(GameObject))
            {
                Comp = tag.gameObject as T;
            }
            else
            {
                Comp = tag.GetComponent<T>();
            }
        }
    }
}