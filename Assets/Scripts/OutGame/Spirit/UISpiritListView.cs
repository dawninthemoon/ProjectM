using UnityEngine;
using UnityEngine.UI;
using UI;
using TMPro;

namespace OutGame
{
    public class UISpiritListView : UIRoot<UISpiritListView>
    {
        [SerializeField] private UISpiritListItem spiritItem;

        public UIElem<RectTransform> ParentHave { get; } = new UIElem<RectTransform>();
        public UIElem<RectTransform> ParentLock { get; } = new UIElem<RectTransform>();

        public UIElem<RectTransform> ParentSpirit { get; } = new UIElem<RectTransform>();

        public UIItemPool SpiritPool { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            SpiritPool = new UIItemPool(spiritItem.gameObject, ParentSpirit.Comp, 20);
        }
    }
}