using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OutGame
{
    public class GrowStoneElement : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI countText;

        public void SetIcon( Sprite sprite )
        {
            icon.sprite = sprite;
        }

        public void SetCountText( int count )
        {
            countText.text = string.Format( "{0}", count );
        }
    }
}