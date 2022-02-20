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

        public UIElem<UIPromotionMaterialPresenter> Material1 = new UIElem<UIPromotionMaterialPresenter>();
        public UIElem<UIPromotionMaterialPresenter> Material2 = new UIElem<UIPromotionMaterialPresenter>();

        public UIElem<Button> ReleaseButton = new UIElem<Button>();

        public void SetSpirit(int key)
        {
            SpiritPreviewImage.Comp.sprite = SpiritIconSpriteControl.Instance.GetSpiritSprite(key);
        }

        public void SetPromotionInfo(PromotionMaterial promotionMaterial)
        {
            Material1.Comp.gameObject.SetActive(promotionMaterial.Count >= 1);
            Material2.Comp.gameObject.SetActive(promotionMaterial.Count >= 2);
        }
    }
}