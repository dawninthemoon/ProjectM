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
        [SerializeField] private Image image;
        [SerializeField] private SpiritIcon spiritIcon;
        [SerializeField] private CharacterIcon characterIcon;

        [SerializeField] private Image selectImage;
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

            if( SlotType == DeckSlotType.Character )
                InitCharacter( key );
            else
                InitSpirit( key );
        }

        public void InitCharacter( int index )
        {
            characterIcon.SetCharacter( index );
        }

        public void InitSpirit( int index )
        {
            spiritIcon.SetSpirit( index );
        }

        public void SetDeck()
        {
            FBControl.FirebaseManager.Instance.UserData.UserDeckData.SetMainSpiritIndex( slotIndex,  objectKey );
        }

        public void OnClick()
        {
            OnSelectEvent?.Invoke( slotIndex );
        }
    }
}