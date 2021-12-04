using UnityEngine;
using FBControl;
using UnityEngine.UI;
using DG.Tweening;

namespace OutGame
{
    public class SpiritDeckControl : DeckControlBase
    {

        public override void Init()
        {
            base.Init();

            for( int i = 0; i < base.deckSlotBases.Length; ++i )
            {
                base.deckSlotBases[i].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(i), i );
            }
        }

        public override void SetActiveSlot( bool isActive )
        {
            for( int i = 0; i < base.deckSlotBases.Length; ++i )
                base.deckSlotBases[i].SetActive( isActive );
        }

        public override void SetDeck( int spiritIndex )
        {
            base.deckSlotBases[currentSlot].SetDeck( spiritIndex );
        }
    }
}