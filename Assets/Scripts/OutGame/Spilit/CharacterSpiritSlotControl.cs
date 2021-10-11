using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void DisableButtons()
    {
        for( int i = 0; i <spilitSlots.Length; ++i )
            spilitSlots[i].DisableButton();
    }
}
