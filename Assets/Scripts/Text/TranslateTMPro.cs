using UnityEngine;
using TMPro;

public class TranslateTMPro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private string key;

    public void Start()
    {
        Translate();
    }

    public void Translate()
    {
        if (key.Equals(""))
            return;

        textMeshProUGUI.text = Data.TextDataContainer.Instance.GetText(key);
    }
}
