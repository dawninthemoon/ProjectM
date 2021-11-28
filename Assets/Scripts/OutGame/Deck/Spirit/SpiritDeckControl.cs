using UnityEngine;
using FBControl;
using UnityEngine.UI;
using DG.Tweening;

namespace OutGame
{
    public class SpiritDeckControl : DeckControlBase
    {
        [Header("Setting")]
        [SerializeField] private SpiritDeckSlot[] deckSlots;

        public override void Init()
        {
            base.Init();

            for( int i = 0; i < deckSlots.Length; ++i )
            {
                deckSlots[i].InitDeckSlot( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(i), i );
            }
        }

        public override void SetActiveSlot( bool isActive )
        {
            for( int i = 0; i < deckSlots.Length; ++i )
                deckSlots[i].SetActive( isActive );
        }

        public override void SetDeck( int spiritIndex )
        {
            deckSlots[currentSlot].SetDeck( spiritIndex );
        }
    }
}