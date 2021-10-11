using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;

namespace OutGame
{
    public class CharacterCard : MonoBehaviour
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private Sprite[] cardSprites;

        [SerializeField] private Stars stars;

        [SerializeField] private Image characterImage;
        [SerializeField] private Transform characterCenter;

        [SerializeField] private TextMeshProUGUI nameText;
        

        private SpriteAtlas characterAtlas;

        public void Init( SpriteAtlas characterAtlas )
        {
            this.characterAtlas = characterAtlas;
        }

        public void SetCard( Data.Character character )
        {
            //characterImage.sprite = characterAtlas.GetSprite( string.Format("Character_{0}", character.Key) );
            //characterImage.rectTransform.pivot = new Vector2( (float)characterImage.sprite.pivot.x / characterImage.sprite.texture.width, (float)characterImage.sprite.pivot.y / characterImage.sprite.texture.height );
            //characterImage.rectTransform.position = characterCenter.position;

            nameText.text = character.Name;
            stars.SetStar( character.Grade );

        }
    }
}