using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

namespace OutGame
{
    public class CharDeckDot : MonoBehaviour
    {
        [SerializeField] private Image standImage;
        [SerializeField] private SpriteAtlas charDotAtlas;
        private ImageAtlasAnimator imageAtlasAnimator = null;

        private bool isInit = false;

        public void Init( int charIndex )
        {
            isInit = true;

            Data.Character charData = Data.CharacterDataParser.Instance.GetCharacter( charIndex );

                Debug.Log( "TRY DATA : " + charIndex );
            if( charData != null)
            {
                Debug.Log( "SET DATA " );
                imageAtlasAnimator = new ImageAtlasAnimator( standImage, charData.SubName + "_", "Idle", true, 0.5f);
            }
            else
                imageAtlasAnimator = null;
        }

        public void Update()
        {
            imageAtlasAnimator?.Progress( charDotAtlas );
        }
    }
}