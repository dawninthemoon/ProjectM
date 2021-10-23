using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUICanvas : MonoBehaviour
{
    private static TopUICanvas instance = null;
    
    public void Awake()
    {
        if( instance != null )
        {
            Destroy( this.gameObject );
            return;
        }
        
        DontDestroyOnLoad( this );
        instance = this;
    }
}
