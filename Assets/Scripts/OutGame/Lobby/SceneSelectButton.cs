using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OutGame
{
    public class SceneSelectButton : MonoBehaviour
    {
        [SerializeField] private Button targetButton;
        [SerializeField] private string sceneName;

        private bool interactable = false;

        public void Awake()
        {
            SceneManager.sceneLoaded += LoadSceneCallback;
        }

        public void OnDestroy()
        {
            SceneManager.sceneLoaded -= LoadSceneCallback;
        }

        public void LoadSceneCallback(Scene scene, LoadSceneMode load)
        {
            if (scene.name.Equals(sceneName))
            {
                targetButton.interactable = interactable;
            }
            else
            {
                targetButton.interactable = !interactable;
            }
        }
    }
}