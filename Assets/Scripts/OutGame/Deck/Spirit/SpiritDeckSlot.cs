using UnityEngine;

namespace OutGame
{
    public class SpiritDeckSlot : DeckSlotBase
    {
        [SerializeField] protected SpiritIcon spiritIcon;

        public override void Init(int index)
        {
            spiritIcon.SetSpirit(index);
        }

        public override void SetDeck(int deckIndex)
        {
            base.SetDeck(deckIndex);
            FBControl.FirebaseManager.Instance.UserData.UserDeckData.SetMainSpiritIndex(slotIndex, deckIndex);
        }
    }
}