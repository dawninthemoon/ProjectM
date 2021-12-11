using UnityEngine;

namespace OutGame
{
    public class CharacterIcon : MonoBehaviour
    {
        [SerializeField] private NativeSizeImage characterImage;

        public void SetCharacter(int character)
        {
            characterImage.SetSprite(ResourceManager.GetInstance().GetSprite(string.Format("Standing/Character_{0}", character)));
        }
    }
}