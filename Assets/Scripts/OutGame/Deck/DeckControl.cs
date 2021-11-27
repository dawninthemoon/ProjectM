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
            deckAbleSpiritScroll.OnSelectEvent += SetSpirit;

            Init();
        }
        
        public void Init()
        {
            for( int i = 0; i < deckSlots.Length; ++i )
            {
                deckSlots[i].OnSelectEvent += OnClickSlotCallback;
                deckSlots[i].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(i), i );
            }
        }

        public void OnClickSlotCallback( int index )
        {
            currentSlot = index;
            if( deckSlots[index].SlotType == DeckSlot.DeckSlotType.Spirit )
            {
                deckAbleSpiritScroll.gameObject.SetActive( true );
            }

            for( int i = 0; i < deckSlots.Length; ++i )
            {
                if( currentSlot == i )
                    deckSlots[i].Select();
                else
                    deckSlots[i].DisSelect();
            }


            Debug.Log( "CLICK SLOT "+ currentSlot );
        }

        public void SetCharacter( int charIndex )
        {
            deckSlots[0].InitDeckSlot( charIndex, 0 );
        }

        public void SetSpirit( int spiritIndex )
        {
            deckSlots[currentSlot].SetDeck( spiritIndex );
            Debug.Log("SET SPRITE : " + currentSlot + "  L "+spiritIndex  );
        }
    }
}