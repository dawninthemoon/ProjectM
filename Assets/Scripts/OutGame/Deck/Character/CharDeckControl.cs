using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBControl;
using UnityEngine.UI;

namespace OutGame
{
    public class CharDeckControl : MonoBehaviour
    {
        [SerializeField] private CharDeckSlotInfo[] charDeckSlotInfos;
        [SerializeField] private CharDeckDot[] charDeckDots;

        [SerializeField] private DeckAbleCharScroll deckAbleCharScroll;
        

        [Header("DeckParents")]
        [SerializeField] private Transform deckListObject;
        [SerializeField] private Transform deckParent;
        [SerializeField] private Image dim;
        [SerializeField] private Button endButton;
    
        public void Start()
        {
            Init();
        }

        public void Init()
        {
            for( int i = 0; i < charDeckSlotInfos.Length; ++i )
            {
                charDeckSlotInfos[i].Init( i, FirebaseManager.Instance.UserData.UserDeckData.GetCharacterIndex( i ) );
                charDeckDots[i].Init( FirebaseManager.Instance.UserData.UserDeckData.GetCharacterIndex( i ) );
            }
        }

        public void ActiveScroll()
        {

        }

        public void DisableScroll()
        {

        }
    }
}