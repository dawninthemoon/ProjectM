using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopUICanvas : MonoBehaviour
{
    private static TopUICanvas instance = null;

    private static readonly string[] DISABLE_SCENE_ARRAY = new string[]
    {
        "GameScene",

    };

    public void Awake()
    {
        if( instance != null )
        {
            Destroy( this.gameObject );
            return;
        }
        
        DontDestroyOnLoad( this );
        SceneManager.activeSceneChanged += ChangedActiveScene;
        instance = this;
    }

    public void SetDestroy()
    {
        SceneManager.activeSceneChanged -= ChangedActiveScene;
        Destroy(this.gameObject);
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        for( int i = 0; i < DISABLE_SCENE_ARRAY.Length; ++i )
        {
            if( DISABLE_SCENE_ARRAY[i] == next.name )
            {
                SetDestroy();
                break;
            }
        }
    }
}
