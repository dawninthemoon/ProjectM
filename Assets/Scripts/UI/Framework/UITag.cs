using UnityEngine;

namespace UI
{
    [ExecuteInEditMode]
    public class UITag : MonoBehaviour
    {
        [SerializeField] private string key;
        public string Key => string.IsNullOrEmpty(key) ? gameObject.name : key;

        #if UNITY_EDITOR
        private void Awake()
        {
            if (!Application.isPlaying)
            {
                GetComponentInParent<UIBehaviour>()?.OnTagUpdated();
            }
        }

        private void OnValidate()
        {
            if (!Application.isPlaying)
                GetComponentInParent<UIBehaviour>()?.OnTagUpdated();
        }

        private void OnDestroy()
        {
            if (!Application.isPlaying)
                GetComponentInParent<UIBehaviour>()?.OnTagUpdated();
        }
        #endif
    }
}