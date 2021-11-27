using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class DeckSlot : MonoBehaviour
    {
        public enum DeckSlotType
        {
            Character,
            Spirit
        }
        [SerializeField] private Image selectImage;
        [SerializeField] private Color selectColor;
        [SerializeField] private Color disSelectColor;

        [SerializeField] private SpiritIcon spiritIcon;
        [SerializeField] private CharacterIcon characterIcon;

        [SerializeField] private DeckSlotType deckSlotType;
        public DeckSlotType SlotType
        {
            get{ return deckSlotType; }
        }

        private int slotIndex = 0;
        private int objectKey = 0;

        public event System.Action<int> OnSelectEvent;
        public event System.Action<int> OnDisSelectEvent;

        public void InitDeckSlot( int key, int slotIndex )
        {
            objectKey = key;
            this.slotIndex = slotIndex;

            RestoreSlot();
        }

        public void RestoreSlot()
        {
            if( SlotType == DeckSlotType.Character )
                InitCharacter( objectKey );
            else
                InitSpirit( objectKey );
        }

        public void InitCharacter( int index )
        {
            characterIcon.SetCharacter( index );
        }

        public void InitSpirit( int index )
        {
            spiritIcon.SetSpirit( index );
        }

        public void SetDeck( int deckIndex )
        {
            FBControl.FirebaseManager.Instance.UserData.UserDeckData.SetMainSpiritIndex( slotIndex,  deckIndex );
            objectKey = deckIndex;
            RestoreSlot();
        }

        public void OnClick()
        {
            Debug.Log(gameObject.name);
            OnSelectEvent?.Invoke( slotIndex );
        }

        public void Select()
        {
            selectImage.color = selectColor;
        }

        public void DisSelect()
        {
            selectImage.color = disSelectColor;
        }
    }
}