using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class UIBehaviour : MonoBehaviour
    {
        private static readonly Dictionary<string, UITag> TagCache = new Dictionary<string, UITag>();
        private static readonly HashSet<string> TagRefCache = new HashSet<string>();

        protected virtual void Awake()
        {
            Localization();

            TagCache.Clear();
            TagRefCache.Clear();

            foreach (var tag in GetComponentsInChildren<UITag>(true))
            {
                if (TagCache.ContainsKey(tag.Key))
                {
                    Debug.LogError($"duplicated key - {tag.Key}", tag.gameObject);
                }
                else
                {
                    TagCache.Add(tag.Key.ToLower(), tag);
                }
            }

            foreach (var prop in GetType().GetBaseClasses(true)
                .SelectMany(type => type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.PropertyType.Name.Contains("UIElem")))
                ) //Generic이라서 그냥 string으로
            {
                var value = prop.GetValue(this);
                SetTagComp(value, prop.Name);
            }

            foreach (var field in GetType().GetBaseClasses(true)
                .SelectMany(type => type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(f => f.FieldType.Name.Contains("UIElem") && !f.Name.Contains("k__BackingField")))
            ) //Generic이라서 그냥 string으로
            {
                var value = field.GetValue(this);
                SetTagComp(value, field.Name);
            }

            foreach (var tag in TagCache)
            {
                if (!TagRefCache.Contains(tag.Key))
                {
                    Debug.LogWarning($"tag never used - {tag.Key}", tag.Value.gameObject);
                }
            }
        }

        private void Localization()
        {
            //foreach (var textComponent in GetComponentsInChildren<Text>(gameObject.transform))
            //{
            //    textComponent.text = Translator.Get(textComponent.text);
            //}
        }

        private void SetTagComp(object instance, string instanceName)
        {
            var tagKey = instance
                .GetType()
                .GetField("_tag", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(instance) as string;

            var optional = (bool)instance
                .GetType()
                .GetField("_optional", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(instance);

            tagKey = tagKey ?? instanceName.Replace("_", "");
            tagKey = tagKey.ToLower();

            if (TagCache.ContainsKey(tagKey))
            {
                instance.GetType()
                    .GetMethod("Set", BindingFlags.Instance | BindingFlags.Public)
                    ?.Invoke(instance, new object[] { TagCache[tagKey] });

                var compProp = instance
                    .GetType()
                    .GetProperty("Comp", BindingFlags.Instance | BindingFlags.Public);

                if (compProp.GetValue(instance) == null)
                {
                    Debug.LogError($"comp not found - {tagKey}, {compProp.PropertyType.Name}");
                }

                TagRefCache.Add(tagKey);
            }
            else if (!optional)
            {
                Debug.LogError($"tag not found - {tagKey}", gameObject);
            }
        }

#if UNITY_EDITOR
        [ShowInInspector] [ReadOnly] private readonly Dictionary<string, UITag> _tags = new Dictionary<string, UITag>();

        private void Register(UITag uiTag)
        {
            if (string.IsNullOrEmpty(uiTag.Key))
                return;

            if (_tags.ContainsKey(uiTag.Key))
            {
                Debug.LogError($"duplicated key - {uiTag.Key}", uiTag.gameObject);
                return;
            }

            _tags.Add(uiTag.Key, uiTag);
        }

        public void OnTagUpdated()
        {
            _tags.Clear();
            foreach (var tag in GetComponentsInChildren<UITag>(true))
                Register(tag);
        }

#endif
    }
}