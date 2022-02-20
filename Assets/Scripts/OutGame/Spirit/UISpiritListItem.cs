using UnityEngine;
using UnityEngine.UI;
using UI;
using TMPro;
using Data;

namespace OutGame
{
    public class UISpiritListItem : UIBehaviour
    {
        public UIElem<TextMeshProUGUI> Level { get; } = new UIElem<TextMeshProUGUI>();
        public UIElem<Image> Icon { get; } = new UIElem<Image>();
        public UIElem<Image> GradeFrame { get; } = new UIElem<Image>();
        public void ActiveButton()
        {
            Level.Comp.gameObject.SetActive(true);
        }

        public void DisableButton()
        {
            Level.Comp.gameObject.SetActive(false);
        }

        public void SetInfo(int lv, int key)
        {
            Level.Comp.text = string.Format("LV\n{0}", lv);
            Icon.Comp.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite(key);
            GradeFrame.Comp.color = PromotionInfo.GetGradeColor((int)SpiritGameData.Get(key).startGrade);
        }

        public void SetInfo(UserSpiritData userSpiritData)
        {
            Level.Comp.text = string.Format("LV\n{0}", userSpiritData.Lv);
            Icon.Comp.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite(userSpiritData.Index);
            GradeFrame.Comp.color = PromotionInfo.GetGradeColor(userSpiritData.Grade);
        }

    }
}