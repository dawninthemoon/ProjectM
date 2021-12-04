using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBControl;
using UnityEngine.UI;

namespace OutGame
{
    public class CharDeckControl : DeckControlBase
    {
        [SerializeField] private CharDeckSlotInfo[] charDeckSlotInfos;
        [SerializeField] private CharDeckDot[] charDeckDots;
    
        public override void Init()
        {
            base.Init();
            
            for( int i = 0; i < charDeckSlotInfos.Length; ++i )
            {
                charDeckSlotInfos[i].Init( i, FirebaseManager.Instance.UserData.UserDeckData.GetCharacterIndex( i ) );
                charDeckDots[i].Init( FirebaseManager.Instance.UserData.UserDeckData.GetCharacterIndex( i ) );

                base.deckSlotBases[i].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.GetCharacterIndex(i), i );
            }
        }

        public override void SetDeck( int spiritIndex )
        {
            base.deckSlotBases[currentSlot].SetDeck( spiritIndex );
        }

        public override void SetActiveSlot( bool isActive )
        {
            for( int i = 0; i < base.deckSlotBases.Length; ++i )
                base.deckSlotBases[i].SetActive( isActive );
        }
    }
}