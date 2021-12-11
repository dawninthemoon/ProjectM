using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Game
}

public static class SceneLoader
{
    public static bool IsSceneLoaded { get; private set; }

    public static SceneType CurrentScene { get; private set; }

    static SceneLoader()
    {
        CurrentScene = SceneType.Game;

        IsSceneLoaded = true;

        SceneManager.sceneLoaded += Loaded;
    }

    public static void Load(SceneType type)
    {
        SceneManager.LoadScene((int)type);

        CurrentScene = type;

        IsSceneLoaded = false;
    }

    public static void Load(int index)
    {
        SceneManager.LoadScene(index);

        CurrentScene = (SceneType)index;

        IsSceneLoaded = false;
    }

    private static void Loaded(Scene scene, LoadSceneMode mode)
    {
        IsSceneLoaded = true;
    }
}
