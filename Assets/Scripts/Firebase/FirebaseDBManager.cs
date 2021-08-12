using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

namespace FBControl
{
    public class FirebaseDBManager
    {
        private FirebaseDatabase userFirebaseDatabase = null;
        private UserDB userDB;

        private bool isInit = false;
        public bool IsInit
        {
            get{ return isInit; }
        }
        public void Init()
        {
            userDB = new UserDB();

            userFirebaseDatabase = FirebaseDatabase.GetInstance("https://prrojectm-default-rtdb.firebaseio.com");
        
            userDB.Init( userFirebaseDatabase.GetReference("user") );
        
            isInit = true;
        }

        public void ReadAll( out UserData userData )
        {
            userData = null;

            //userFirebaseDatabase.
        }
    }
}