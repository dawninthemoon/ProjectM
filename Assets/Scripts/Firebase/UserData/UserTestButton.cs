using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTestButton : MonoBehaviour
{
    public void AddCharacter()
    {
        FBControl.FirebaseManager.Instance.UserData.GetCharacter( 101 );
        FBControl.FirebaseManager.Instance.UserDB.SetCharacter();

        FBControl.FirebaseManager.Instance.UserData.UserCurrenyData.Gold += 100;
    }
}
