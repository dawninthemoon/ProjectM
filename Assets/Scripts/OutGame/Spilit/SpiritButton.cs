using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;

public class SpiritButton : MonoBehaviour, ICell
{
    [SerializeField] private SpiritIcon spilitIcon;
    private SpiritInfoUI spilitInfoUI;
    private int spritIndex = 0;
    
    public void Init( int index, SpiritInfoUI spilitInfoUI )
    {
        spritIndex = index;
        spilitIcon.SetSpirit( index );

        this.spilitInfoUI = spilitInfoUI;
    }

    public void OnClick()
    {
        spilitInfoUI.gameObject.SetActive( true );
        spilitInfoUI.SetInfo( Data.SpiritDataParser.Instance.GetSpiritData( spritIndex ) );
    }
}
