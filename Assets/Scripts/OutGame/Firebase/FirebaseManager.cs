using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBControl
{
    public class FirebaseManager : MonoBehaviour
    {
        private static FirebaseManager instance;
        public static FirebaseManager Instance
        {
            get{ return instance; }
        }

        private FirebaseDBManager firebaseDBManager = new FirebaseDBManager();
        public FirebaseDBManager FirebaseDBManager
        {
            get{ return firebaseDBManager; }
        }

        [SerializeField] private UserDB userDB;
        public UserData UserData
        {
            get{ return userDB.userData; }
        }

        private FirebaseAuthManager firebaseAuthManager = new FirebaseAuthManager();
        public FirebaseAuthManager FirebaseAuthManager
        {
            get{ return firebaseAuthManager; }
        }

        public event System.Action OnLoadConpleteEvent;

        public void Awake()
        {
            DontDestroyOnLoad( this );
            instance = this;
        }

        public void Init()
        {
            StartCoroutine( InitRoutine() );
        }

        public IEnumerator InitRoutine()
        {
            firebaseAuthManager.Init();
            yield return new WaitUntil( ()=> { return firebaseAuthManager.IsInit; } );

            Debug.Log("Auth 로그인 완료");
            userDB.Init();
        
            yield return new WaitUntil( ()=> { return userDB.IsLoaded; } );
            Debug.Log("DB INIT 완료");
            OnLoadConpleteEvent?.Invoke();
        }
    }
}