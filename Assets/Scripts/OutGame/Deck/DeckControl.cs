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
            for( int i = 0; i < deckSlots.Length; ++i )
            {
                deckSlots[i].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(i), i );
            }
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