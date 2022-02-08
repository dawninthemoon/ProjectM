using UnityEngine;
using UnityEngine.UI;
using UI;
using TMPro;

namespace OutGame
{
    public class UISpiritListItem : UIBehaviour
    {
        public UIElem<TextMeshProUGUI> Level { get; } = new UIElem<TextMeshProUGUI>();
        public UIElem<Image> Icon { get; } = new UIElem<Image>();


        public void ActiveButton()
        {
            Level.Comp.gameObject.SetActive(true);
        }

        public void DisableButton()
        {
            Level.Comp.gameObject.SetActive(false);
        }
    }
}