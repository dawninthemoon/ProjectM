using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace OutGame
{
    public class CharacterButtonScroll : MonoBehaviour
    {
        [SerializeField] private CharacterButton[] characterButtons;
        
        public void Awake()
        {
            Init();
        }

        public void Init()
        {
            for( int i = 0; i < characterButtons.Length; ++i )
            {
                if( CharacterDataParser.Instance.Data.Length > i )
                {
                    characterButtons[i].SetCharacter( CharacterDataParser.Instance.Data[i] );
                    characterButtons[i].gameObject.SetActive( true );
                }
                else
                {
                    characterButtons[i].gameObject.SetActive( false );
                }
            }
        }
    }
}