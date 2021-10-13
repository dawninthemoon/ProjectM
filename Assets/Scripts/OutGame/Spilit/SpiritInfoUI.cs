using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Data;
using FBControl;

public class SpiritInfoUI : MonoBehaviour
{
    [SerializeField] private CharacterSpiritSlotControl characterSpiritSlotControl;

    [Header("InfoUI")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private Image expSlider;
    
    private SpiritData spiritData;
    private UserSpiritData userSpiritData;
    
    public void SetInfo( SpiritData spiritData )
    {
        this.spiritData = spiritData;
        nameText.text = spiritData.Name;
        icon.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite( spiritData.Key );

        userSpiritData = FirebaseManager.Instance.UserData.UserSpiritDataList.Data.Find( (x) => { return x.Index == spiritData.Key; } );

        SetLevelText();

        expSlider.fillAmount = userSpiritData.Exp / 100f;
    }

    public void SetLevelText()
    {
        levelText.text = string.Format( "Lv.{0}", userSpiritData.Lv );
    }

    public void AddLevel()
    {
        FirebaseManager.Instance.UserData.UserSpiritDataList.SetLevel( spiritData.Key, userSpiritData.Lv + 1 );
        SetLevelText();
    }

    public void SetSpirit()
    {
        characterSpiritSlotControl.ActiveButtons( spiritData );
    }
}
