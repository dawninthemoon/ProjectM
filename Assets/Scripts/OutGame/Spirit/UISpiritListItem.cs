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

        public UIElem<TextMeshProUGUI> SoulCount { get; } = new UIElem<TextMeshProUGUI>();
        public UIElem<Image> SoulCountFill { get; } = new UIElem<Image>();

        public UIElem<RectTransform> ParentStar { get; } = new UIElem<RectTransform>();
        public UIElem<RectTransform> DisableDim { get; } = new UIElem<RectTransform>();

        public UIElem<Button> BtnSelect { get; } = new UIElem<Button>();

        public void ActiveButton()
        {
            DisableDim.Comp.gameObject.SetActive(false);
            Level.Comp.gameObject.SetActive(true);
            ParentStar.Comp.gameObject.SetActive(true);
        }

        public void DisableButton()
        {
            DisableDim.Comp.gameObject.SetActive(true);
            Level.Comp.gameObject.SetActive(false);
            ParentStar.Comp.gameObject.SetActive(false);
        }
    }
}