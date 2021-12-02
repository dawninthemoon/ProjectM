using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace OutGame
{
    public class CharDeckSlotInfo : MonoBehaviour
    {
        [SerializeField] private CharacterIcon characterIcon;

        public void Init( int index, int charIndex )
        {
            characterIcon.SetCharacter( charIndex );
        }
    }
}