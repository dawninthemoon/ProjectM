using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Data;

namespace OutGame
{
    public class CharacterButton : MonoBehaviour
    {
        [SerializeField] private Image characterImage;
        [SerializeField] private NativeSizeImage characterNativeSizeImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI subTitleText;

        [SerializeField] private Stars stars;

        public void SetCharacter( Character characterData )
        {
            characterImage.sprite = ResourceManager.GetInstance().GetSprite( string.Format("Standing/Character_{0}", characterData.Key ) );
            characterNativeSizeImage.SetNativeSize();

            nameText.text = characterData.Name;
            subTitleText.text = characterData.SubName;

            stars.SetStar( characterData.Grade );
        }
    }
}