using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCard : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite[] cardSprites;

    [SerializeField] private Image characterImage;

    [SerializeField] private TextMeshProUGUI nameText;
    
    public void SetCard( Data.Character character )
    {
        cardImage.sprite = cardSprites[character.Grade - 1];

        nameText.text = character.Name;
    }
}