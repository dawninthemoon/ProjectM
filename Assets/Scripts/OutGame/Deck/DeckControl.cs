using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBControl;
namespace OutGame
{
    public class DeckControl : MonoBehaviour
    {
        [SerializeField] private DeckSlot[] deckSlots;

        public void Start()
        {
            Init();
        }
        
        public void Init()
        {
            deckSlots[0].SetCharacter( FirebaseManager.Instance.UserData.UserDeckData.CharIndex );
            deckSlots[1].SetSpirit( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(0) );
            deckSlots[2].SetSpirit( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(1) );
            deckSlots[3].SetSpirit( FirebaseManager.Instance.UserData.UserDeckData.GetMainSpiritIndex(2) );
        }

        public void SetCharacter( int charIndex )
        {
            deckSlots[0].SetCharacter( charIndex );
        }

        public void SetSpirit( int spiritIndex, int slot )
        {
            deckSlots[slot].SetSpirit( spiritIndex );
        }
    }
}