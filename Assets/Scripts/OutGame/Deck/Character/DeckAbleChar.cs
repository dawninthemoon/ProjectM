using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class DeckAbleChar : DeckScrollButtonBase
    {
        [SerializeField] private CharacterIcon characterIcon;
        [SerializeField] private CharDeckDot charDeckDot;
        
        public override void Init( int index, int objectKey, System.Action<int> onClickCallback )
        {
            base.Init( index, objectKey, onClickCallback );
            characterIcon.SetCharacter( objectKey );
            charDeckDot.Init( objectKey );
        }
    }
}