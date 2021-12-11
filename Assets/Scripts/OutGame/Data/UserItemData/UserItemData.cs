using Boomlagoon.JSON;
using FBControl;
using System;
using System.Collections.Generic;

public class UserItemData
{
    public int Key;
    public int Count = 0;
}

public class UserItemDataContainer
{
    private Dictionary<int, int> userItemDictionary = new Dictionary<int, int>();

    public Dictionary<int, int> UserItemDictionary
    {
        get { return userItemDictionary; }
    }

    public void Parse(JSONArray jsonArray)
    {
        for (int i = 0; i < jsonArray.Length; ++i)
        {
            JSONObject obj = jsonArray[i].Obj;

            if (obj.ContainsKey("Key"))
                userItemDictionary.Add((int)obj.GetNumber("Key"), (int)obj.GetNumber("Count"));
        }
    }

    public JSONArray ToJsonArray()
    {
        JSONArray jsonArray = new JSONArray();

        int[] keys = userItemDictionary.Keys.ToArray();

        for (int i = 0; i < keys.Length; ++i)
        {
            JSONObject jsonObject = new JSONObject();

            jsonObject.Add("Key", keys[i]);
            jsonObject.Add("Count", userItemDictionary[keys[i]]);

            jsonArray.Add(jsonObject);
        }

        return jsonArray;
    }

    public void AddItem(int key, int count)
    {
        if (userItemDictionary.ContainsKey(key))
        {
            userItemDictionary[key] += count;
        }
        else
        {
            userItemDictionary.Add(key, count);
        }

        int index = Array.IndexOf(userItemDictionary.Keys.ToArray(), key);

        JSONObject obj = new JSONObject();

        obj.Add("Key", key);
        obj.Add("Count", userItemDictionary[key]);

        FirebaseManager.Instance.UserDB.SaveChildrenJsonData(obj.ToString(), "itemData", index.ToString());
    }
}