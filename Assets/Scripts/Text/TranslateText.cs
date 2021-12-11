using UnityEngine;
using UnityEngine.UI;

public class TranslateText : MonoBehaviour
{
    [SerializeField] private Text text;

    [SerializeField] private string key;

    public void Start()
    {
        Translate();
    }

    public void Translate()
    {
        text.text = Data.TextDataContainer.Instance.GetText(key);
    }
}