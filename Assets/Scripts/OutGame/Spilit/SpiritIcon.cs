using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritIcon : MonoBehaviour
{
    [SerializeField] private Image spritIcon;
    [SerializeField] private NativeSizeImage nativeSizeImage;

    public void SetSpirit( int index )
    {
        spritIcon.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite( index );
        
        if( spritIcon.sprite == null )
            spritIcon.color = Color.clear;
        else
            spritIcon.color = Color.white;

        nativeSizeImage?.SetNativeSize();
    }

    public void SetNull()
    {
        spritIcon.sprite = null;
        spritIcon.color = Color.clear;
    }

    public void SetSpirit( Data.SpiritData spiritData )
    {
        spritIcon.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite( spiritData.Key );

        if( spritIcon.sprite == null )
            spritIcon.color = Color.clear;
        else
            spritIcon.color = Color.white;
        nativeSizeImage?.SetNativeSize();
    }
}
