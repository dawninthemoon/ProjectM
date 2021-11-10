using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagButton : MonoBehaviour
{
    [SerializeField] private Button buttonSource;

    private int index = 0;
    
    public event System.Action<int> OnSelectEvent;
    public event System.Action<int> OnDisEvent;

    public void Init( int index, System.Action<int> onSelectCallback )
    {
        this.index = index;
        this.OnSelectEvent += onSelectCallback;
    }

    public void SetSelect()
    {
        buttonSource.interactable = false;
        OnSelectEvent?.Invoke( index );
    }

    public void DisSelect()
    {
        buttonSource.interactable = true;
        OnDisEvent?.Invoke( index );
    }
}
