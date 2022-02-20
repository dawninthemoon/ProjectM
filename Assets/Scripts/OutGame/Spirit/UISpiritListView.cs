using UnityEngine;
using UnityEngine.UI;
using UI;
using TMPro;

namespace OutGame
{
    public class UISpiritListView : UIRoot<UISpiritListView>
    {
        [SerializeField] private UISpiritListItem spiritItem;

        public UIElem<RectTransform> ParentSpirit { get; } = new UIElem<RectTransform>();
        public UIElem<Button> PromotionButton { get; } = new UIElem<Button>();

        public UIItemPool SpiritPool { get; private set; }

        private UIPromotionPresenter uiPromotionPresenter;

        protected override void Awake()
        {
            base.Awake();

            SpiritPool = new UIItemPool(spiritItem.gameObject, ParentSpirit.Comp, 20);

            PromotionButton.Comp.onClick.AddListener(()=> { ShowPromotionView(); });
        }

        public void ShowPromotionView()
        {
            uiPromotionPresenter = UIPromotionView.Instance.Show<UIPromotionPresenter>();
            uiPromotionPresenter.View.PromotionSpiritListPrecenter.Comp.ViewAllSpirit();
        }
    }
}