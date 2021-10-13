using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FBControl;

public class CharacterSpiritSlotControl : MonoBehaviour
{
    [SerializeField] private CharacterSpritSlot[] spilitSlots;

    private Data.SpiritData readySpiritData;

    public void Awake()
    {
        for( int i = 0; i < spilitSlots.Length; ++i )
        {
            spilitSlots[i].Init( i );
        }

        for( int i = 0; i < spilitSlots.Length; ++i )
        {
            int index = FirebaseManager.Instance.UserData.UserSpiritDataList.GetUserSlot( i );

            Data.SpiritData data = Data.SpiritDataParser.Instance.GetSpiritData( index );

            if( data != null)
                spilitSlots[i].SetSpritData( data );
            else
                spilitSlots[i].SetNull();
        }
    }

    public void ActiveButtons( Data.SpiritData spiritData )
    {
        readySpiritData = spiritData;

        for( int i = 0; i <spilitSlots.Length; ++i )
            spilitSlots[i].ActiveButton();
    }

    public void SetSpirit( int index )
    {
        spilitSlots[index].SetSpritData( readySpiritData );
        
        DisableButtons();

        FirebaseManager.Instance.UserData.UserSpiritDataList.SetUserSlot( index, readySpiritData.Key );
    }

    public void DisableButtons()
    {
        for( int i = 0; i <spilitSlots.Length; ++i )
            spilitSlots[i].DisableButton();
    }
}
