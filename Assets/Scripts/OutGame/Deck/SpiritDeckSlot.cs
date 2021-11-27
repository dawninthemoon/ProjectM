using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class SpiritDeckSlot : MonoBehaviour
    {
        [SerializeField] private Image selectImage;
        [SerializeField] private Color selectColor;
        [SerializeField] private Color disSelectColor;

        [SerializeField] private SpiritIcon spiritIcon;

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
            InitSpirit( objectKey );
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