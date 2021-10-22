using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR

public class AutoPlayModeSceneSetup
{
    static AutoPlayModeSceneSetup()
    {
        if( EditorBuildSettings.scenes.Length <= 1 )
            return;

        string path = EditorBuildSettings.scenes[0].path;
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
    }
}

#endif