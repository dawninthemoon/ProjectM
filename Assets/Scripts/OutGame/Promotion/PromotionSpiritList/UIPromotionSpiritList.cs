using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Data;

namespace OutGame
{
    public class UIPromotionSpiritList : UIBehaviour
    {
        [SerializeField] private UIPromotionItem spiritItem;

        public UIElem<RectTransform> ParentSpirit { get; } = new UIElem<RectTransform>();

        private UIItemPool spiritPool;
        public UIItemPool SpiritPool { get { Initialize(); return spiritPool; } }

        private bool isInit = false;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            if(isInit)
            {
                return;
            }
            spiritPool = new UIItemPool(spiritItem.gameObject, ParentSpirit.Comp, 20);
            isInit = true;
        }

        public void SetList()
        {
        }
    }
}