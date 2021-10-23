using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUIBackButton : MonoBehaviour
{
    private static TopUIBackButton instance;
    public static TopUIBackButton Instance
    {
        get{ return instance; }
    }

    private Stack<System.Action> OnClickCallback = new Stack<System.Action>();

    public void Awake()
    {
        if( instance != null )
            return;
        SetInstance();
    }

    private void SetInstance()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        if( instance == this)
            instance = null;
    }

    public void AddCallback( System.Action action )
    {
        OnClickCallback.Push( action );
        //Debug.Log( "Push: "+OnClickCallback.Count );
        
        gameObject.SetActive( true );
    }

    public void PopCallback()
    {
        OnClickCallback.Pop();
        //Debug.Log( "POP: "+OnClickCallback.Count );
        
        if( OnClickCallback.Count == 0 )
            gameObject.SetActive( false );
    }

    public void OnClick()
    {
        OnClickCallback.Peek()?.Invoke();
    }
}
