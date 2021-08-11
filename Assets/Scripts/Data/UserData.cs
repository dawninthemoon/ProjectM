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
    
    public JSONObject GetJsonFile()
    {
        JSONObject jsonResult = new JSONObject();

        jsonResult.Add( "nickName", nickName );
        jsonResult.Add( "uid", uid );
        jsonResult.Add( "lastLoginTime", lastLoginTime.ToString() );//해외 현지화 해야함 임시 ToString();
        
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
    }
}
