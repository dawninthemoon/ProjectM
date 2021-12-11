using UnityEngine;

namespace OutGame
{
    public class CharDeckSlot : DeckSlotBase
    {
        [SerializeField] private CharDeckDot charDeckDot;
        [SerializeField] private CharDeckSlotInfo charDeckSlotInfo;

        public override void Init(int index)
        {
            // spiritIcon.SetSpirit( index );
            charDeckDot.Init(index);
            charDeckSlotInfo.Init(base.slotIndex, index);
        }

        public override void SetDeck(int deckIndex)
        {
            base.SetDeck(deckIndex);
            FBControl.FirebaseManager.Instance.UserData.UserDeckData.SetCharacterIndex(slotIndex, deckIndex);
        }
    }
}