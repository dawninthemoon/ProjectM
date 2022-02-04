using UnityEngine;
using UnityEngine.UI;
using UI;

namespace OutGame
{
    public class UISpiritListItem : UIBehaviour
    {
        public UIElem<Text> Level { get; } = new UIElem<Text>();
        public UIElem<Image> Icon { get; } = new UIElem<Image>();

        public UIElem<Text> SoulCount { get; } = new UIElem<Text>();
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