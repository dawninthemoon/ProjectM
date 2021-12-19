using FBControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private GameObject anonymousLoginButton;
        [SerializeField] private GameObject loginPopup;

        public void Start()
        {
            FirebaseManager.Instance.OnLoadCompleteEvent += ToLobby;

            FirebaseManager.Instance.Init();
        }

        public void OnClickAnonymousLogin()
        {
            loginPopup.gameObject.SetActive(true);
            FirebaseManager.Instance.FirebaseAuthManager.LoginedAnonymous();
        }
        public void OnClickGoogleLogin()
        {
            loginPopup.gameObject.SetActive(true);
            FirebaseManager.Instance.FirebaseAuthManager.GoogleLogin();
        }

        public void ToLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}