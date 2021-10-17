using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTestButton : MonoBehaviour
{
    public void AddCharacter()
    {
        for( int i = 0; i < 13; ++i )
        {
            UserSpiritData spilitData = new UserSpiritData();
            spilitData.Lv = 1;
            spilitData.Index = 21001 + i;
            spilitData.Exp = 0;

            FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.Add( spilitData );
        }
        for( int i = 0; i < 13; ++i )
        {
            UserSpiritData spilitData = new UserSpiritData();
            spilitData.Lv = 1;
            spilitData.Index = 22001 + i;
            spilitData.Exp = 0;

            FBControl.FirebaseManager.Instance.UserData.UserSpiritDataList.Data.Add( spilitData );
        }

        FBControl.FirebaseManager.Instance.UserData.GetCharacter( 101 );
        FBControl.FirebaseManager.Instance.UserData.GetCharacter(102);
        FBControl.FirebaseManager.Instance.UserData.GetCharacter(103);
        FBControl.FirebaseManager.Instance.UserData.GetCharacter(104);
        FBControl.FirebaseManager.Instance.UserData.GetCharacter(105);
        FBControl.FirebaseManager.Instance.UserData.GetCharacter(106);
        FBControl.FirebaseManager.Instance.UserDB.SetCharacter();
        FBControl.FirebaseManager.Instance.UserDB.SetSpilitData();

        FBControl.FirebaseManager.Instance.UserData.UserCurrenyData.Gold += 100;
    }
}
