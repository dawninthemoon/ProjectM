using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Data;

public class GachaItemIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;

    public void SetIcon( RandomBoxData randomBoxData )
    {
        switch( randomBoxData.RewardType )
        {
            case RewardType.Character:
                Character character = CharacterDataParser.GetCharacter( randomBoxData.RewardKey );
                nameText.text = character.Name;
            break;
            case RewardType.Item:
                ItemData item = ItemDataParser.GetItemData( randomBoxData.RewardKey );
                nameText.text = item.Name;
            break;
        }
    }
}
