using Boomlagoon.JSON;
using FBControl;
using System.Collections.Generic;
using Data;

public class UserSpiritData
{
    public int Index = 0;
    public int Lv;
    public int Exp;

    public int Grade = 0;

    public JSONObject ToJsonObject()
    {
        JSONObject jsonObject = new JSONObject();

        jsonObject.Add("Index", Index);
        jsonObject.Add("Lv", Lv);
        jsonObject.Add("Exp", Exp);
        jsonObject.Add("Grade", Grade);

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

        if (jsonObject.ContainsKey("Grade"))
            Grade = (int)jsonObject.GetNumber("Grade");
        
        if(Grade == 0)
        {
            SpiritGameData spiritGameData = System.Array.Find(SpiritGameData.All, x => { return x.key == Index; });

            if (spiritGameData != null)
            {
                Grade = (int)spiritGameData.startGrade;
            }
        }
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

    public void SetGrade(int arrayIndex, int grade)
    {
        data[arrayIndex].Grade = grade;
        FBControl.FirebaseManager.Instance.UserDB.SaveChildrenData(grade, "spiritData", arrayIndex.ToString(), "Grade");
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