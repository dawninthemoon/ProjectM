using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif

public class LoginProvider
{

    public static void InitLoginProvider()
    {
#if UNITY_ANDROID

        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
               .RequestIdToken()
               .RequestEmail()
               .Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

#endif
    }

#if UNITY_ANDROID
    public static void LoginGoogle(Action<bool> callback )
    {
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("LoginGoogle Social.localUser.Authenticate success : " + success);
                callback.Invoke(true);
            }
            else
            {
                callback.Invoke(false);
                Debug.Log("Authentication failed");
            }
        });
    }
#endif

#if UNITY_IOS
    public static void LoginApple(Action<CallbackResult> _callback)
    {
        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
        //IOS -> key를 안보내도 되는건지?
        Social.localUser.Authenticate(success => {
            if (success)
            {
                _callback.Invoke(CallbackResult.SUCCESS);
            }
            else
            {

                _callback.Invoke(CallbackResult.FAIL);
                Debug.Log("Failed to authenticate");
            }
        });
    }
#endif
}
