using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
// using GooglePlayGames;
// using GooglePlayGames.BasicApi;
//using AppleAuth.Interfaces;
using System.Text;
// using UnityEngine.SignInWithApple;
//using AppleAuth.Extensions;

namespace FBControl
{
    public class FirebaseAuthManager
    {
        private FirebaseAuth auth;

        private FirebaseUser user;
        public FirebaseUser User
        {
            get { return user; }
        }

        private bool isInit = false;
        public bool IsInit
        {
            get{return isInit;}
        }

        public void Init()
        {
            auth = FirebaseAuth.DefaultInstance;
        }

        public void LoginedLogin()
        {
            user = auth.CurrentUser;
            isInit = true;
        }

        #if UNITY_ANDROID
        public void GoogleLogined()
        {
            Debug.Log("FirebaseAuthManager GoogleLogined 여기서 안지나간다고?");
        // StartCoroutine(TryFirebaseLogin());
        }

        public void TryGoogleLogout()
            {

                if (Social.localUser.authenticated) // 로그인 되어 있다면
                {
                    // PlayGamesPlatform.Instance.SignOut(); // Google 로그아웃
                    auth.SignOut(); // Firebase 로그아웃
                }
            }


        // IEnumerator TryFirebaseLogin()
        //     {
        //         Debug.Log(" FirebaseAuthManager TryFirebaseLogin 여기서 안지나간다고?");
        //         while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
        //             yield return null;
        //         //yield return null;
        //         string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        //         Debug.Log(" FirebaseAuthManager TryFirebaseLogin idToken : " + idToken);

        //         Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        //         auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        //         {
        //             if (task.IsCanceled)
        //             {
        //                 Debug.LogError("SignInWithCredentialAsync was canceled.");
        //                 return;
        //             }
        //             if (task.IsFaulted)
        //             {
        //                 Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
        //                 return;
        //             }

        //             Debug.Log(" auth.CurrentUser.Email " + auth.CurrentUser.Email);
        //             Debug.Log("Success!");

        //             user = auth.CurrentUser;
        //             isInit = true;
        //         });
        //     }
        #endif


        // public void AppleLogined(UserInfo userInfo, string rawNonce)
        // {
        //     //var rawNonce = GenerateRandomString(32);
        //     //var nonce = GenerateSHA256NonceFromRawNonce(rawNonce);

        //   Debug.Log("파이어베이스 로그인 시도");
        //     Credential credential =    OAuthProvider.GetCredential("apple.com", userInfo.idToken, rawNonce, null);
        //     auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
        //         if (task.IsCanceled)
        //         {
        //             Debug.LogError("SignInWithCredentialAsync was canceled.");
        //             return;
        //         }
        //         if (task.IsFaulted)
        //         {
        //             Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
        //             return;
        //         }

        //         Debug.Log("Firebase Login 성공!!");


        //         user = task.Result;
        //         isInit = true;

        //     });
        // }




        public void LoginedAnonymous()
        {
            
            auth.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInAnonymouslyAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log(" Sussess Anonymous : " + task.Result.UserId);

                user = task.Result;
                isInit = true;
            });
        }

        public bool CheckIsAnonymous()
        {
            FirebaseUser user = auth.CurrentUser;
            return user.IsAnonymous;

        }

        public bool CheckLoginState()
        {
            FirebaseUser user = auth.CurrentUser;
            
            if (user != null)
                return true;

            return false;
        }

        public void FirebaseSignOut()
        {
            isInit = false;



            auth.SignOut();
        }
    }
}