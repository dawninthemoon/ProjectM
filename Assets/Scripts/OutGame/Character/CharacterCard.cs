using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

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

        public void Init(SpriteAtlas characterAtlas)
        {
            this.characterAtlas = characterAtlas;
        }

        public void SetCard(Data.Character character)
        {
            if (character == null)
                return;

            characterImage.sprite = ResourceManager.GetInstance().GetSprite(string.Format("Standing/Character_{0}", character.Key));

            if (characterImage.sprite != null)
            {
                Sprite targetSprite = characterImage.sprite;
                characterImage.rectTransform.pivot = new Vector2((float)targetSprite.pivot.x / targetSprite.texture.width, (float)targetSprite.pivot.y / targetSprite.texture.height);
            }

            characterImage.transform.localPosition = Vector3.zero;

            //characterImage.rectTransform.position = characterCenter.position;

            nameText.text = character.Name;
            stars.SetStar(character.Grade);
        }
    }
}