using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBControl;
namespace OutGame
{
    public class DeckControl : MonoBehaviour
    {
        [SerializeField] private DeckSlot[] deckSlots;
        [SerializeField] private DeckAbleSpiritScroll deckAbleSpiritScroll;

        private int currentSlot = -1;

        public void Start()
        {
            Init();
        }
        
        public void Init()
        {
            deckSlots[0].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.CharIndex, 0 );
            deckSlots[1].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(0), 1 );
            deckSlots[2].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(1), 2 );
            deckSlots[3].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(2), 3 );
        }

        public void OnClickSlotCallback( int index )
        {
            if( deckSlots[index].SlotType == DeckSlot.DeckSlotType.Spirit )
            {
                deckAbleSpiritScroll.gameObject.SetActive( true );
            }
        }

        public void SetCharacter( int charIndex )
        {
            deckSlots[0].InitDeckSlot( charIndex, 0 );
        }

        public void SetSpirit( int spiritIndex, int slot )
        {
            deckSlots[slot].InitDeckSlot( spiritIndex, slot );
        }
    }
}