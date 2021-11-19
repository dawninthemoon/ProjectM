using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBControl;
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
            loginPopup.gameObject.SetActive( true );
            FirebaseManager.Instance.FirebaseAuthManager.LoginedAnonymous();
        }

        public void ToLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}