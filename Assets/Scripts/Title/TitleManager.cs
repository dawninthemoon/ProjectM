using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBControl;

namespace Title
{
    public class TitleManager : MonoBehaviour
    {
        public void Awake()
        {
            FirebaseManager.Instance.Init();
        }

        public void OnClickAnonymousLogin()
        {
            FirebaseManager.Instance.FirebaseAuthManager.LoginedAnonymous();
        }
    }
}