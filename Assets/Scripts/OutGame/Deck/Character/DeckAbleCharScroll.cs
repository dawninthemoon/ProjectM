using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class DeckAbleCharScroll : DeckScrollBase
    {
        [SerializeField] private DeckAbleChar[] deckAbleChars;

        private Data.Character[] characters;

        public event System.Action<int> OnClickEvent;

        public void Start()
        {
            characters = Data.CharacterDataParser.Instance.Data;
            Init();
        }

        public override void Init()
        {
            for( int i = 0; i < deckAbleChars.Length; ++i )
            {
                if( i >= characters.Length )
                    deckAbleChars[i].gameObject.SetActive( false );
                else
                {
                    deckAbleChars[i].gameObject.SetActive( true );
                    deckAbleChars[i].OnClickEvent += OnClickCallback;
                    deckAbleChars[i].Init( i, characters[i].Key );
                }
            }
        }   

        public void OnClickCallback( int index )
        {
            OnClickEvent?.Invoke( index );
        }
    }
}