using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Boomlagoon.JSON;

//파이어 베이스 서버에 저장할 UserData
public class UserData
{
    private string nickName;
    public string NickName
    {
        get{ return nickName; }
    }

    private string uid;
    public string Uid
    {
        get{ return uid; }
    }    

    private DateTime lastLoginTime;
    public DateTime LastLoginTime
    {
        get{ return lastLoginTime; }
    }

    private List<UserCharacterData> charData = new List<UserCharacterData>();
    public List<UserCharacterData> CharData
    {
        get{ return charData; }
    }

    private UserCurrenyData userCurrenyData = new UserCurrenyData();
    public UserCurrenyData UserCurrenyData
    {
        get{ return userCurrenyData; }
    }

    private UserItemDataContainer userItemDataContainer = new UserItemDataContainer();
    public UserItemDataContainer UserItemDataContainer
    {
        get{ return userItemDataContainer; }
    }

    private UserSpiritDataList userSpiritDataList = new UserSpiritDataList();
    public UserSpiritDataList UserSpiritDataList
    {
        get
        { 
            return userSpiritDataList; 
        }
    }

    private int[] userDeckData = new int[3];
    public int[] UserDeckData
    {
        get{ return userDeckData; }
        set{ userDeckData = value;}
    }

    public void SetDefaultData()
    {
        nickName = "Normal";
        uid = FBControl.FirebaseManager.Instance.FirebaseAuthManager.User.UserId;

        lastLoginTime = DateTime.Now;

        userCurrenyData.Gold = 0;
        userCurrenyData.FCash = 0;
        userCurrenyData.PCash = 0;
    }
    
    #region GetJson
    public JSONObject GetJsonFile()
    {
        JSONObject jsonResult = new JSONObject();

        JSONArray jsonArray = new JSONArray();
        JSONObject jsonTemp = new JSONObject();

        jsonResult.Add( "nickName", nickName );
        jsonResult.Add( "uid", uid );
        jsonResult.Add( "lastLoginTime", lastLoginTime.ToString() );//해외 현지화 해야함 임시 ToString();
        jsonResult.Add( "gold", userCurrenyData.Gold );
        jsonResult.Add( "fCash", userCurrenyData.FCash );
        jsonResult.Add( "pCash", userCurrenyData.PCash );

        jsonResult.Add( "charData", GetCharJsonArray() );

        jsonResult.Add("deck", GetDeckJsonArray() );

        jsonResult.Add("itemData", userItemDataContainer.ToJsonArray() );

        jsonResult.Add("spiritData", userSpiritDataList.ToJsonObject() );

        return jsonResult;
    }

    public JSONArray GetDeckJsonArray()
    {
        JSONArray jsonArray = new JSONArray();

        for( int i = 0; i < userDeckData.Length; ++i )
        {
            jsonArray.Add( userDeckData[i] );
        }

        return jsonArray;
    }

    public JSONArray GetCharJsonArray()
    {
        JSONArray jsonArray = new JSONArray();

        for( int i = 0; i < charData.Count; ++i )
        {
            jsonArray.Add( charData[i].ToJsonObject() );
        }

        return jsonArray;
    }

    #endregion

    #region SetJson

    public void SetJsonFile( JSONObject jsonObject )
    {
        if( jsonObject.ContainsKey("nickName") )
            nickName = jsonObject.GetString("nickName");
        if( jsonObject.ContainsKey("uid") )
            uid = jsonObject.GetString("uid");
        if( jsonObject.ContainsKey("lastLoginTime") )
            lastLoginTime = DateTime.Parse( jsonObject.GetString("lastLoginTime") );
        if( jsonObject.ContainsKey("gold") )
            userCurrenyData.Gold = (int)jsonObject.GetNumber("gold");
        if( jsonObject.ContainsKey("fCash") )
            userCurrenyData.FCash = (int)jsonObject.GetNumber("fCash");
        if( jsonObject.ContainsKey("PCash") )
            userCurrenyData.PCash = (int)jsonObject.GetNumber("pCash");

        charData.Clear();

        if( jsonObject.ContainsKey("charData") )
        {
            JSONArray array = jsonObject.GetArray("charData");

            for( int i = 0; i < array.Length; ++i )
            {
                UserCharacterData dataTemp = new UserCharacterData();

                dataTemp.SetJsonObject( array[i].Obj );
                charData.Add( dataTemp );
            }
        }

        if( jsonObject.ContainsKey("deck") )
        {
            JSONArray array = jsonObject.GetArray("deck");

            userDeckData = new int[array.Length];

            for( int i = 0; i < array.Length; ++i )
            {
                userDeckData[i] = (int)array[i].Number;
            }
        }

        if( jsonObject.ContainsKey("itemData") )
            userItemDataContainer.Parse( jsonObject.GetArray("itemData") );

        if( jsonObject.ContainsKey("spiritData") )
            userSpiritDataList.SetJsonObject( jsonObject.GetArray("spiritData") );
    }

    public void GetCharacter( int index )
    {
        UserCharacterData userCharacter = charData.Find( (x)=> x.Index == index );

        if( userCharacter != null)
        {
        }
        else
        {
            userCharacter = new UserCharacterData( index );
            charData.Add( userCharacter );
        }
    }

    #endregion
}
