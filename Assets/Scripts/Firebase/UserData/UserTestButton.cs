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

        for( int i = 0; i < 13; ++i )
        {
            UserSpiritData spilitData = new UserSpiritData();
            spilitData.Lv = 1;
            spilitData.Index = 2400001 + i;
            spilitData.Exp = 0;

            FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.Add( spilitData );
        }
        for( int i = 0; i < 13; ++i )
        {
            UserSpiritData spilitData = new UserSpiritData();
            spilitData.Lv = 1;
            spilitData.Index = 2300001 + i;
            spilitData.Exp = 0;

            FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.Add( spilitData );
        }

        FBControl.FirebaseManager.Instance.UserData.GetCharacter( 101 );
        FBControl.FirebaseManager.Instance.UserDB.SetCharacter();
        FBControl.FirebaseManager.Instance.UserDB.SaveDeck();
        FBControl.FirebaseManager.Instance.UserDB.SetSpilitData();

        FBControl.FirebaseManager.Instance.UserData.UserCurrenyData.Gold += 100;
    }
}
