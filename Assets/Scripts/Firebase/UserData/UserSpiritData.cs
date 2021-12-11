using Boomlagoon.JSON;
using FBControl;
using System.Collections.Generic;

public class UserSpiritData
{
    public int Index;
    public int Lv;
    public int Exp;

    public int Soul;
    public byte Star;

    public JSONObject ToJsonObject()
    {
        JSONObject jsonObject = new JSONObject();

        jsonObject.Add("Index", Index);
        jsonObject.Add("Lv", Lv);
        jsonObject.Add("Exp", Exp);
        jsonObject.Add("Soul", Soul);
        jsonObject.Add("Star", Star);

        return jsonObject;
    }

    public void SetJsonObject(JSONObject jsonObject)
    {
        if (jsonObject.ContainsKey("Index"))
            Index = (int)jsonObject.GetNumber("Index");

        if (jsonObject.ContainsKey("Lv"))
            Lv = (int)jsonObject.GetNumber("Lv");

        if (jsonObject.ContainsKey("Exp"))
            Exp = (int)jsonObject.GetNumber("Exp");

        if (jsonObject.ContainsKey("Soul"))
            Soul = (int)jsonObject.GetNumber("Soul");

        if (jsonObject.ContainsKey("Star"))
            Star = (byte)jsonObject.GetNumber("Star");
    }
}

public class UserSpiritDataList
{
    private static readonly string USER_SPIRIT_FORMAT = "{0}_SLOT_USER_SPIRIT";
    private List<UserSpiritData> data = new List<UserSpiritData>();

    public List<UserSpiritData> Data
    {
        get { return data; }
    }

    public UserSpiritData FindData(int index)
    {
        return data.Find((x) => { return x.Index == index; });
    }

    public JSONArray ToJsonObject()
    {
        JSONArray jsonArray = new JSONArray();

        for (int i = 0; i < data.Count; ++i)
            jsonArray.Add(data[i].ToJsonObject());

        return jsonArray;
    }

    public void SetJsonObject(JSONArray jsonArray)
    {
        for (int i = 0; i < jsonArray.Length; ++i)
        {
            UserSpiritData spilitData = new UserSpiritData();
            spilitData.SetJsonObject(jsonArray[i].Obj);

            data.Add(spilitData);
        }
    }

    public void SetLevel(int index, int level)
    {
        for (int i = 0; i < data.Count; ++i)
        {
            if (data[i].Index == index)
            {
                data[i].Lv = level;
                FBControl.FirebaseManager.Instance.UserDB.SaveChildrenData(level, "spiritData", i.ToString(), "Lv");
                break;
            }
        }
    }

    public void SetSoul(int index, int soul)
    {
        int findIndex = data.FindIndex((x) => { return x.Index == index; });

        if (findIndex < 0)
            return;

        UserSpiritData targetData = data[findIndex];

        targetData.Soul = soul;
        FBControl.FirebaseManager.Instance.UserDB.SaveChildrenData(soul, "spiritData", findIndex.ToString(), "Soul");
    }

    public void SetUserSlot(int index, int spilitIndex)
    {
        FirebaseManager.Instance.UserDB.SaveLocalData(string.Format(USER_SPIRIT_FORMAT, index), spilitIndex);
    }

    public int GetUserSlot(int index)
    {
        return FirebaseManager.Instance.UserDB.GetLocalIntigerData(string.Format(USER_SPIRIT_FORMAT, index));
    }
}