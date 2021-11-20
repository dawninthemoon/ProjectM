using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class DeckSlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private SpiritIcon spiritIcon;

        public void SetCharacter( int index )
        {

        }

        public void SetSpirit( int index )
        {
            spiritIcon.SetSpirit( index );
        }
    }
}