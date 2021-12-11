using Firebase.Database;

namespace FBControl
{
    public class FirebaseDBManager
    {
        private FirebaseDatabase userFirebaseDatabase = null;

        private bool isInit = false;

        public bool IsInit
        {
            get { return isInit; }
        }

        public void Init()
        {
            userFirebaseDatabase = FirebaseDatabase.GetInstance("https://prrojectm-default-rtdb.firebaseio.com/");
            userFirebaseDatabase.SetPersistenceEnabled(false);
        }

        public void ReadAll(out UserData userData)
        {
            userData = null;

            //userFirebaseDatabase.
        }
    }
}