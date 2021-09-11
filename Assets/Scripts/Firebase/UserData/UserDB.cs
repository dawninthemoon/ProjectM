using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Boomlagoon.JSON;
//using CodeStage.AntiCheat.ObscuredTypes;
using System;

namespace FBControl
{
    public class UserDB : MonoBehaviour
    {
        public UserData userData;
        private DatabaseReference databaseReference;
        private bool isLoaded = false;
        public bool IsLoaded
        {
            get{ return isLoaded; }
        }

        bool isChecked = false;
        private string userID;

        //샤딩한 경우 여기서 분리시키기
        public void Init( ) 
        {
            userData = new UserData();

            FirebaseDatabase database = FirebaseDatabase.DefaultInstance;
            database.SetPersistenceEnabled( false );
            this.databaseReference = database.GetReference("/user/"+ FirebaseManager.Instance.FirebaseAuthManager.User.UserId);
            
            //userID =;
            StartCoroutine( LoadDataRoutine() );
        }

        public IEnumerator LoadDataRoutine()
        {
            isLoaded = false;
            string userId = "";
            bool isFound = false;
            if (FirebaseManager.Instance.FirebaseAuthManager.User.IsAnonymous)
                userId = SystemInfo.deviceUniqueIdentifier;
            else
                userId = FirebaseManager.Instance.FirebaseAuthManager.User.UserId;

            //databaseReference = databaseReference.Child(userId);

            Debug.Log("TRY");
            
            databaseReference.GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    userData.SetDefaultData();
                }
                else if (task.IsCompleted)
                {
                    if (task.Result.Exists)
                    {
                        DataSnapshot snapshot = task.Result;
                        userData.SetJsonFile( JSONObject.Parse( snapshot.GetRawJsonValue() ) );
                        Debug.Log("BTYPE");

                        isFound = true;  
                    }
                }

                isChecked= true;
            });

            yield return new WaitUntil( ()=>{ return isChecked; } );
            Debug.Log("TRYb");

            if( !isFound )
            {
                userData.SetDefaultData();
                SaveUserData();
            }

            isLoaded = true;
        }


        public void EnterHandleValueChanged()
        {
            Debug.Log("EnterHandleValueChanged 등록 등록" );
            databaseReference.Child("lastLoginTime")
        .ValueChanged += HandleLastJoinValueChanged;
        }

        void HandleLastJoinValueChanged(object sender, ValueChangedEventArgs args)
        {
        
            DateTime changeTime;
            string changeTimeStr = args.Snapshot.GetRawJsonValue();
            if (changeTimeStr == null)
                return;
            if (changeTimeStr.Length >= 2)
            {
                changeTimeStr = args.Snapshot.GetRawJsonValue().Substring(1, args.Snapshot.GetRawJsonValue().Length - 2);
            }
            

            if (!DateTime.TryParse(changeTimeStr, out changeTime))
            {
                return;
            }
            

            DateTime _newTime = DateTime.Parse( args.Snapshot.GetRawJsonValue().Substring(1, args.Snapshot.GetRawJsonValue().Length - 2));

            // TimeSpan gap = _newTime - User.Instance.userLoginTime;


            // if (User.Instance.userLoginTime == null)
            //     return;
            // if (gap.TotalSeconds >0)
            // {
            //     PopUpManager.Instance.ShowPopUp(PopUpType.WORNING, "중복 로그인", "중복 로그인되었습니다. 게임이 종료됩니다.", () => { Application.Quit(); });
            // }
        }
        
        //캐릭터 덱 저장
        public void SaveDeck()
        {
            DatabaseReference propertyReference = databaseReference.Child("deck");
            propertyReference.SetRawJsonValueAsync( userData.GetDeckJsonArray().ToString() );
        }

        //캐릭터 정보 저장
        public void SetCharacter()
        {
            DatabaseReference propertyReference = databaseReference.Child("charData");
            propertyReference.SetRawJsonValueAsync( userData.GetCharJsonArray().ToString() );
        }

        ///<summary> 유저 데이터의 하위 프로퍼티 찾아서 조정 </summary>
        public void SaveChildrenData( object index, params string[] parents )
        {
            DatabaseReference propertyReference = databaseReference;//Child(propertyName);

            for (int i = 0; i < parents.Length; ++i)
            {
                propertyReference = propertyReference.Child(parents[i]);
            }

            propertyReference.SetValueAsync(index);
        }
        
        public void SaveChildrenJsonData( string index, params string[] parents )
        {
            DatabaseReference propertyReference = databaseReference;//Child(propertyName);

            for (int i = 0; i < parents.Length; ++i)
            {
                propertyReference = propertyReference.Child(parents[i]);
            }

            propertyReference.SetRawJsonValueAsync(index);
        }

        ///<summary> 유저 데이터의 하위 프로퍼티만 조정 </summary>
        public void SaveChildrenData(string propertyName, object index)
        {
            DatabaseReference propertyReference = databaseReference.Child(propertyName);
            propertyReference.SetValueAsync(index);
        }

        ///<summary> 유저 데이터 전체를 조정 </summary>
        public void SaveUserData()
        {
            string json = userData.GetJsonFile().ToString();//JsonUtility.ToJson(User.Instance.userData);
            
            databaseReference.SetRawJsonValueAsync(json);
        }


        ///<summary> 유저 데이터의 하위 프로퍼티만 받음 </summary>
        public string LoadChildrenDataString(string propertyName)
        {
            string strFile = "";

            // DatabaseReference propertyReference = databaseReference.Child(USER_DATA_DB_NAME).Child(User.Instance.userData.userId).Child(propertyName);

            // propertyReference.GetValueAsync().ContinueWith(task =>
            // {
            //     if (task.IsFaulted)
            //     {
            //         strFile = "";
            //     }
            //     else if (task.IsCompleted)
            //     {
            //         DataSnapshot snapshot = task.Result;

            //         strFile = snapshot.Value.ToString();
            //     }
            // });

            return strFile;
        }

        ///<summary>로컬 데이터 저장</summary>
        public void SaveLocalData(string propertyName, int index)
        {
            PlayerPrefs.SetInt(propertyName, index);
            PlayerPrefs.Save();
        }
        public void SaveLocalData(string propertyName, string index)
        {
            PlayerPrefs.SetString(propertyName, index);
            PlayerPrefs.Save();
        }

        ///<summary>로컬 데이터 불러오기</summary>
        public int GetLocalIntigerData(string propertyName)
        {
            return PlayerPrefs.GetInt(propertyName, 0);
        }
        public string GetLocalStringData(string propertyName)
        {
            return PlayerPrefs.GetString(propertyName, "");
        }

    }
}