using FBControl;
using TMPro;
using UnityEngine;

public class StaminaText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI staminaText;

    public void Start()
    {
        SetText();
    }

    public void SetText()
    {
        UserStaminaData staminaData = FirebaseManager.Instance.UserData.UserStaminaData;
        staminaText.text = string.Format("{0}/{1}", staminaData.Stamina, staminaData.MaxStamina);
    }
}