using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTestButton : MonoBehaviour
{
    public void AddCharacter()
    {
        FBControl.FirebaseManager.Instance.UserData.UserDeckData[0] = 101;
        FBControl.FirebaseManager.Instance.UserData.UserDeckData[1] = 102;
        FBControl.FirebaseManager.Instance.UserData.UserDeckData[2] = 103;
        FBControl.FirebaseManager.Instance.UserData.UserDeckData[3] = 104;

        FBControl.FirebaseManager.Instance.UserData.GetCharacter( 101 );
        FBControl.FirebaseManager.Instance.UserDB.SetCharacter();
        FBControl.FirebaseManager.Instance.UserDB.SaveDeck();

        FBControl.FirebaseManager.Instance.UserData.UserCurrenyData.Gold += 100;
    }
}
