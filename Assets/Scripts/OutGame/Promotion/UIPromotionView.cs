using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using TMPro;

namespace OutGame
{
    public class UIPromotionView : UIRoot<UIPromotionView>
    {
        public UIElem<TextMeshProUGUI> NameText { get; } = new UIElem<TextMeshProUGUI>();
        public UIElem<Image> SpiritPreviewImage { get; } = new UIElem<Image>();

        public UIElem<UIPromotionSpiritList> PromotionSpiritList = new UIElem<UIPromotionSpiritList>();
        public UIElem<UIPromotionSpiritListPresenter> PromotionSpiritListPrecenter = new UIElem<UIPromotionSpiritListPresenter>();

        public void SetSpirit(int key)
        {
            SpiritPreviewImage.Comp.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite(key);
        }
    }
}