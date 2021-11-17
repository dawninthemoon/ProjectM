using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagButtonList : MonoBehaviour
{
    [SerializeField] private TagButton[] tagButtons;

    public event System.Action<int> OnButtonClickEvent;

    public void Awake()
    {
        for( int i = 0; i < tagButtons.Length; ++i )
        {
            tagButtons[i].Init( i, OnClickCallback );
        }
    }

    public void OnClickCallback( int index )
    {
        for( int i = 0;  i < tagButtons.Length; ++i )
        {
            if( i != index )
                tagButtons[i].DisSelect();
        }

        OnButtonClickEvent?.Invoke( index );
    }

    public void SetIndex( int index )
    {
        for( int i = 0;  i < tagButtons.Length; ++i )
        {
            if( i != index )
                tagButtons[i].DisSelect();
            else
                tagButtons[i].SetSelect();
        }
        
        OnButtonClickEvent?.Invoke( index );
    }
}
