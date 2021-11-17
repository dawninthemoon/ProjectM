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

    public event System.Action OnActiveEvent;
    public event System.Action OnDisableEvent;
    
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

    public void SetActive()
    {
        OnActiveEvent?.Invoke();
        gameObject.SetActive( true );
        TopUIBackButton.Instance.AddCallback( () => { this.SetDisable(); } );
    }

    public void SetDisable()
    {
        OnDisableEvent?.Invoke();
        gameObject.SetActive( false );
        TopUIBackButton.Instance.PopCallback();
    }

    public void SetLevelText()
    {
        if( userSpiritData != null )
            levelText.text = string.Format( "Lv.{0}", userSpiritData.Lv );
        else
            levelText.text = string.Format( "Lv.{0}", "NULL" );
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
