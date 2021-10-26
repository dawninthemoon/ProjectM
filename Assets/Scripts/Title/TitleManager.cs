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

        public void Start()
        {
            FirebaseManager.Instance.OnLoadConpleteEvent += ToLobby;

            FirebaseManager.Instance.Init();
        }

        public void OnClickAnonymousLogin()
        {
            FirebaseManager.Instance.FirebaseAuthManager.LoginedAnonymous();
        }

        public void ToLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}