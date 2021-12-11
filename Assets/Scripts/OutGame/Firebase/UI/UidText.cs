using TMPro;
using UnityEngine;

public class UidText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uidText;

    public void Start()
    {
        uidText.text = FBControl.FirebaseManager.Instance.FirebaseAuthManager.User.UserId;
    }
}