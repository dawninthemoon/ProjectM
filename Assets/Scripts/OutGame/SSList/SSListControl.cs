using UnityEngine;

namespace OutGame
{
    public class SSListControl : MonoBehaviour
    {
        public enum SSListElementType
        {
            Character = 0,
            Spirit = 1
        }

        [SerializeField] private SpiritButtonScroll spiritButtonScroll;
        [SerializeField] private CharacterButtonScroll characterButtonScroll;

        [SerializeField] private TagButtonList tagButtonList;

        public void OnEnable()
        {
            SetScroll(1);
            tagButtonList.SetIndex(1);

            tagButtonList.OnButtonClickEvent += SetScroll;
        }

        public void SetScroll(int index)
        {
            SSListElementType ssListElementType = (SSListElementType)index;

            characterButtonScroll.gameObject.SetActive(ssListElementType == SSListElementType.Character);
            spiritButtonScroll.gameObject.SetActive(ssListElementType == SSListElementType.Spirit);
        }
    }
}