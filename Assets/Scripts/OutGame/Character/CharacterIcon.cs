using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class CharacterIcon : MonoBehaviour
    {
        [SerializeField] private Image characterImage;

        public void SetCharacter(int character)
        {
            characterImage.sprite = ResourceManager.GetInstance().GetSprite(string.Format("Standing/Character_{0}", character));
        }
    }
}