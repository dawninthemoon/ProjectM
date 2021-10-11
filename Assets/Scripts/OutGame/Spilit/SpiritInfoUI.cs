using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Data;
public class SpiritInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;

    [SerializeField] private CharacterSpiritSlotControl characterSpiritSlotControl;
    
    private SpiritData spiritData;
    
    public void SetInfo( SpiritData spiritData )
    {
        this.spiritData = spiritData;
        nameText.text = spiritData.Name;
        icon.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite( spiritData.Key );
    }

    public void SetSpirit()
    {
        characterSpiritSlotControl.ActiveButtons( spiritData );
    }
}
