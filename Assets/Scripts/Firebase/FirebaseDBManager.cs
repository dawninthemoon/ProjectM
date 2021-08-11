using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

namespace FBControl
{
    public class FirebaseDBManager : MonoBehaviour
    {
        private FirebaseDatabase userFirebaseDatabase = null;
        private UserDB userDB;
        public void Init()
        {
            userDB = new UserDB();

            userFirebaseDatabase = FirebaseDatabase.GetInstance("https://prrojectm-default-rtdb.firebaseio.com");
        
            userDB.Init( userFirebaseDatabase.GetReference("user") );
        

        }

        public void ReadAll( out UserData userData )
        {
            userData = null;

            //userFirebaseDatabase.
        }
    }
}