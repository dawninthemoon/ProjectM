using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;

namespace OutGame
{
    public class UIPromotionItem : UIBehaviour
    {
        [SerializeField] private UISpiritListItem uiSpiritListItem;
        public UISpiritListItem UISpiritListItem
        {
            get { return uiSpiritListItem; }
        }

        public UIElem<Button> Button { get; } = new UIElem<Button>();
    }
}