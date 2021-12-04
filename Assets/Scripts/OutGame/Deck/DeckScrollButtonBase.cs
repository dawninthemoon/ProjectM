using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class DeckScrollButtonBase : MonoBehaviour
    {
        protected int objectKey;
        protected int index;

        public event System.Action<int> OnClickEvent;

        public virtual void Init( int index, int objectKey, System.Action<int> onClickCallback )
        {
            this.objectKey = objectKey;
            this.index = index;

            OnClickEvent += onClickCallback;
        }

        public virtual void OnClick()
        {
            OnClickEvent?.Invoke( objectKey );
        }
    }
}