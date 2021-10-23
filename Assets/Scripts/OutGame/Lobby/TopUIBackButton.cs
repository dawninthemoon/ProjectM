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

    private Stack<System.Action> OnClickCallbackStack = new Stack<System.Action>();

    public void Awake()
    {
        if (instance != null)
        {
            instance.OnClickCallbackStack.Clear();
            instance.gameObject.SetActive(false);
            return;
        }
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
        OnClickCallbackStack.Push( action );
        //Debug.Log( "Push: "+OnClickCallback.Count );
        
        gameObject.SetActive( true );
    }

    public void PopCallback()
    {
        OnClickCallbackStack.Pop();
        //Debug.Log( "POP: "+OnClickCallback.Count );
        
        if( OnClickCallbackStack.Count == 0 )
            gameObject.SetActive( false );
    }

    public void OnClick()
    {
        OnClickCallbackStack.Peek()?.Invoke();
    }
}
