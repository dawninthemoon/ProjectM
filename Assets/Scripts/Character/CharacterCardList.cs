using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using UnityEngine.U2D;

namespace OutGame
{
    public class CharacterCardList : MonoBehaviour
    {
        [SerializeField] private CharacterCard[] characterCardPool;
        [SerializeField] private SpriteAtlas characterAtlas;
        
        public void Start()
        {
            Init();
        }

        public void Init()
        {
            for( int i = 0; i < characterCardPool.Length; ++i )
            {
                characterCardPool[i].Init( characterAtlas );

                if( i >= CharacterDataParser.CharacterData.Length )
                    characterCardPool[i].gameObject.SetActive( false );
                else
                {
                    characterCardPool[i].gameObject.SetActive( true );

                    characterCardPool[i].SetCard( CharacterDataParser.CharacterData[i] );
                }
            }
        }
    }
}