using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using FBControl;
public class GachaLogic
{
    public RandomBoxData Gacha( int index )
    {
        GachaData gachaData = GachaDataParser.GachaData[ index ];

        RandomBoxData data = RandomBoxDataParser.GetRandomBoxResult( gachaData.RandomBoxKey );

        return data;
    }

    public RandomBoxData Gacha( GachaData gachaData )
    {
        RandomBoxData data = RandomBoxDataParser.GetRandomBoxResult( gachaData.RandomBoxKey );

        return data;
    }

    public RandomBoxData[] Gacha( GachaData gachaData, int count )
    {
        RandomBoxData[] data = RandomBoxDataParser.GetRandomBoxResultArray( gachaData.RandomBoxKey, count );

        return data;
    }

    public void GetItem( RandomBoxData data )
    {
        FirebaseManager.Instance.UserData.UserItemDataContainer.AddItem( data.RewardKey, data.Count );
    }
}
