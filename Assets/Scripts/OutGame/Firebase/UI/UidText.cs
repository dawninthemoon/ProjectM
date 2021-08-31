using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UidText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uidText;

    public void Start()
    {
        uidText.text = FBControl.FirebaseManager.Instance.FirebaseAuthManager.User.UserId;
    }   
}
