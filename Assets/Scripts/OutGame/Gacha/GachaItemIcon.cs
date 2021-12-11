using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;

    public void SetIcon(RandomBoxData randomBoxData)
    {
        switch (randomBoxData.RewardType)
        {
            case RewardType.Character:
                Character character = CharacterDataParser.Instance.GetCharacter(randomBoxData.RewardKey);
                nameText.text = character.Name;
                break;

            case RewardType.Item:
                ItemData item = ItemDataParser.Instance.GetItemData(randomBoxData.RewardKey);
                nameText.text = item.Name;
                break;
        }
    }
}