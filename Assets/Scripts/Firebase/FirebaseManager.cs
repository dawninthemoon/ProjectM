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

        private FirebaseDBManager firebaseDBManager;
        public FirebaseDBManager FirebaseDBManager
        {
            get{ return firebaseDBManager; }
        }

        private FirebaseAuthManager firebaseAuthManager;
        public FirebaseAuthManager FirebaseAuthManager
        {
            get{ return firebaseAuthManager; }
        }

        public void Awake()
        {
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
            firebaseDBManager.Init();
        }
    }
}