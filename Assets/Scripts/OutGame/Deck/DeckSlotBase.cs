using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public abstract class DeckSlotBase : MonoBehaviour
    {
        [SerializeField] protected Image selectImage;
        [SerializeField] protected Color selectColor;
        [SerializeField] protected Color disSelectColor;

        [SerializeField] protected Button deckButton;

        protected int slotIndex = 0;
        protected int objectKey = 0;

        public event System.Action<int> OnSelectEvent;

        public event System.Action<int> OnDisSelectEvent;

        public void InitDeckSlot(int key, int slotIndex)
        {
            objectKey = key;
            this.slotIndex = slotIndex;

            RestoreSlot();
        }

        public void RestoreSlot()
        {
            Init(objectKey);
        }

        public abstract void Init(int index);

        public virtual void SetDeck(int deckIndex)
        {
            objectKey = deckIndex;
            RestoreSlot();
        }

        public void OnClick()
        {
            Debug.Log("CLICK! ");
            OnSelectEvent?.Invoke(slotIndex);
        }

        public void Select()
        {
            selectImage.color = selectColor;
        }

        public void DisSelect()
        {
            selectImage.color = disSelectColor;
        }

        public void SetActive(bool isActive)
        {
            deckButton.interactable = isActive;

            if (!isActive)
                Select();
        }
    }
}