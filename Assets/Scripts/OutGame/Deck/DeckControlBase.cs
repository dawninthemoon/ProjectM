using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OutGame
{
    public abstract class DeckControlBase : MonoBehaviour
    {
        [SerializeField] protected DeckSlotBase[] deckSlotBases;
        [SerializeField] protected DeckScrollBase deckScroll;
        [SerializeField] protected RectTransform scrollActiveTransform;
        [SerializeField] protected RectTransform scrollDisableTransform;

        [Header("DeckParents")]
        [SerializeField] protected Transform deckListObject;
        [SerializeField] protected Transform deckParent;
        [SerializeField] protected Image dim;
        [SerializeField] protected Button endButton;

        protected int currentSlot = -1;
        public void Start()
        {
            deckScroll.OnSelectEvent += SetDeck;

            Init();
        }

        public virtual void Init()
        {
            for( int i = 0; i < deckSlotBases.Length; ++i )
            {
                deckSlotBases[i].OnSelectEvent += OnClickSlotCallback;   
            }
        }
        public abstract void SetDeck( int spiritIndex );

        public virtual void ActiveScroll()
        {
            dim.gameObject.SetActive( true );
            dim.color = Color.clear;

            dim.DOColor( new Color( 0, 0, 0, 200f/255f ), .4f );

            deckListObject.transform.parent = dim.transform;

            deckScroll.gameObject.SetActive( true );
            deckScroll.transform.position = scrollDisableTransform.position;
            deckScroll.transform.DOMove( scrollActiveTransform.position, .4f );

            endButton.gameObject.SetActive( true );
            endButton.onClick.AddListener( DisableScroll );
            
            SetActiveSlot( true );
        }

        public virtual void DisableScroll()
        {
            dim.DOColor( Color.clear, .4f ).OnComplete( () => {
                dim.gameObject.SetActive( false );
                deckListObject.transform.parent = deckParent;
                deckScroll.gameObject.SetActive( false );
            });
            
            deckScroll.transform.position = scrollActiveTransform.position;
            deckScroll.transform.DOMove( scrollDisableTransform.position, .4f );

            endButton.gameObject.SetActive( false );
            endButton.onClick.RemoveListener( DisableScroll );

            SetActiveSlot( false );
        }

        public abstract void SetActiveSlot( bool isActive );

        public void OnClickSlotCallback( int index )
        {
            currentSlot = index;

            for( int i = 0; i < deckSlotBases.Length; ++i )
            {
                if( currentSlot == i )
                    deckSlotBases[i].Select();
                else
                    deckSlotBases[i].DisSelect();
            }
        }

    }
}