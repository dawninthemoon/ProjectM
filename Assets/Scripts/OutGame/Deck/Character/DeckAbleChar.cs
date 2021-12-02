using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class DeckAbleChar : MonoBehaviour
    {
        [SerializeField] private CharacterIcon characterIcon;
        [SerializeField] private CharDeckDot charDeckDot;

        private int index;

        public event System.Action<int> OnClickEvent;

        public void Init( int index, int charIndex )
        {
            this.index = index;
            characterIcon.SetCharacter( charIndex );
        }   

        public void OnClick()
        {
            OnClickEvent?.Invoke( index );
        }
    }
}