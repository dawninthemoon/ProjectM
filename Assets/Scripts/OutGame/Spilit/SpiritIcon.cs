using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritIcon : MonoBehaviour
{
    [SerializeField] private Image spritIcon;

    public void SetSpirit( int index )
    {
        spritIcon.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite( index );
    }

    public void SetSpirit( Data.SpiritData spiritData )
    {
        spritIcon.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite( spiritData.Key );
    }
}
