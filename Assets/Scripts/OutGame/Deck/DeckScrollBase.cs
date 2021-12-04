using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public abstract class DeckScrollBase : MonoBehaviour
    {
        [SerializeField] protected DeckScrollButtonBase[] deckScrollButtonBases;
        public event System.Action<int> OnSelectEvent;

        public abstract void Init();

        public void OnSelectCallback( int index )
        {
            OnSelectEvent?.Invoke( index );
        }

    }
}