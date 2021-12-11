using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class SpiritExpSlider : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI lvText;

        public void SetSpirit(UserSpiritData userSpiritData, SpiritData spiritData)
        {
            fillImage.fillAmount = (float)userSpiritData.Exp / SpiritExpParser.Instance.GetSpiritExpToLV(userSpiritData.Lv);
            lvText.text = string.Format("LV. {0}/{1}", userSpiritData.Lv, SpiritExpParser.MaxLevel);
        }
    }
}