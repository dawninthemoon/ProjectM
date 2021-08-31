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

    private int gold;
    public int Gold
    {
        get{ return gold; }
    }

    private int cash;
    public int Cash
    {
        get{ return cash; }
    }

    private Dictionary<int,int> characterDictionary = new Dictionary<int, int>();
    public Dictionary<int,int> CharacterDictionary
    {
        get{ return characterDictionary; }
    }

    public void SetDefaultData()
    {
        nickName = "Normal";
        uid = FBControl.FirebaseManager.Instance.FirebaseAuthManager.User.UserId;

        lastLoginTime = DateTime.Now;

        gold = 0;
        cash = 0;
    }
    
    public JSONObject GetJsonFile()
    {
        JSONObject jsonResult = new JSONObject();

        JSONArray jsonArray = new JSONArray();
        JSONObject jsonTemp = new JSONObject();

        jsonResult.Add( "nickName", nickName );
        jsonResult.Add( "uid", uid );
        jsonResult.Add( "lastLoginTime", lastLoginTime.ToString() );//해외 현지화 해야함 임시 ToString();
        jsonResult.Add( "gold", gold );
        jsonResult.Add( "cash", cash );

        int[] charKeys = characterDictionary.Keys.ToArray();

        for( int i = 0; i < charKeys[i]; ++i )
        {
            jsonTemp.Clear();
            
            jsonTemp.Add( "code", charKeys[i] );
            jsonTemp.Add( "count", characterDictionary[charKeys[i]] );
            
            jsonArray.Add( jsonTemp );
        }

        jsonResult.Add( "character", jsonArray );
        
        return jsonResult;
    }

    public void SetJsonFile( JSONObject jsonObject )
    {
        if( jsonObject.ContainsKey("nickName") )
            nickName = jsonObject.GetString("nickName");
        if( jsonObject.ContainsKey("uid") )
            uid = jsonObject.GetString("uid");
        if( jsonObject.ContainsKey("lastLoginTime") )
            lastLoginTime = DateTime.Parse( jsonObject.GetString("lastLoginTime") );
        if( jsonObject.ContainsKey("gold") )
            gold = (int)jsonObject.GetNumber("gold");
        if( jsonObject.ContainsKey("cash") )
            cash = (int)jsonObject.GetNumber("cash");

        if( jsonObject.ContainsKey("character") )
        {
            JSONArray arrTemnp = jsonObject.GetArray("character");
            JSONObject jsonObj;
            characterDictionary.Clear();

            for( int i = 0; i < arrTemnp.Length; ++i )
            {
                jsonObj = arrTemnp[i].Obj;
                characterDictionary.Add( (int)jsonObject.GetNumber("code"), (int)jsonObject.GetNumber("count") );
            }
        }
    }
}
