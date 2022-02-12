using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Data;

namespace OutGame
{
    public class UIPromotionSpiritList : UIBehaviour
    {
        [SerializeField] private UISpiritListItem spiritItem;

        public UIElem<RectTransform> ParentSpirit { get; } = new UIElem<RectTransform>();

        public UIItemPool SpiritPool { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            SpiritPool = new UIItemPool(spiritItem.gameObject, ParentSpirit.Comp, 20);
        }

        public void SetList()
        {
        }
    }
}