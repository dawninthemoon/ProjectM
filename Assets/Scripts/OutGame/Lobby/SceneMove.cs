using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void OnLoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}