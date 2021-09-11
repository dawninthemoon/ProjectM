using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using FBControl;
public class GachaRogic : MonoBehaviour
{
    public void Gacha( int index )
    {
        GachaData gachaData = GachaDataParser.GachaData[ index ];

        FirebaseManager.Instance.UserData.UserCurrenyData.PCash -= gachaData.CashValue;

        int getItem = RandomBoxDataParser.GetRandomBoxResult( gachaData.RandomBoxKey );
    
        Debug.Log( ItemDataParser.GetItemData( getItem ).Name );
    }
}
