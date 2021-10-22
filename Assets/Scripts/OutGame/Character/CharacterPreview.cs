using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class CharacterPreview : MonoBehaviour
    {
        [SerializeField] private Image characterImage;
        [SerializeField] private NativeSizeImage nativeSizeImage;

        public void Start()
        {
            SetInfo(FBControl.FirebaseManager.Instance.UserData.CurrentCharacter);
        }

        public void SetInfo( int characterIndex )
        {
            characterImage.sprite 
                = ResourceManager.GetInstance().GetSprite(string.Format("Standing/Character_{0}", characterIndex));

            nativeSizeImage.SetNativeSize();
        }
    }
}