using Boomlagoon.JSON;

public class UserCharacterData
{
    public int Index;
    public int Lv;
    public int BreakLv; //돌파 레벨
    public int AwakeLv; //각성 레벨
    // public int Spirit1;
    // public int Spirit2;
    // public int Spirit3;
    // public int Spirit4;
    // public int Rigging1;
    // public int Rigging2;
    // public int Rigging3;
    // public int Rigging4;
    // public int Rigging5;
    // public int Rigging6;

    public UserCharacterData()
    {
    }

    public UserCharacterData(int index)
    {
        Index = index;
        Lv = 1;
        BreakLv = 1;
        AwakeLv = 1;
    }

    //아티팩트
    public JSONObject ToJsonObject()
    {
        JSONObject jsonObject = new JSONObject();

        jsonObject.Add("Index", Index);
        jsonObject.Add("Lv", Lv);
        jsonObject.Add("BreakLv", BreakLv);
        jsonObject.Add("AwakeLv", AwakeLv);

        // jsonObject.Add("Spirit1", Spirit1);
        // jsonObject.Add("Spirit2", Spirit2);
        // jsonObject.Add("Spirit3", Spirit3);
        // jsonObject.Add("Spirit4", Spirit4);

        // jsonObject.Add("Rigging1", Rigging1);
        // jsonObject.Add("Rigging2", Rigging2);
        // jsonObject.Add("Rigging3", Rigging3);
        // jsonObject.Add("Rigging4", Rigging4);
        // jsonObject.Add("Rigging5", Rigging5);
        // jsonObject.Add("Rigging6", Rigging6);

        return jsonObject;
    }

    public void SetJsonObject(JSONObject jsonObject)
    {
        if (jsonObject.ContainsKey("Index"))
            Index = (int)jsonObject.GetNumber("Index");

        if (jsonObject.ContainsKey("Lv"))
            Lv = (int)jsonObject.GetNumber("Lv");

        if (jsonObject.ContainsKey("BreakLv"))
            BreakLv = (int)jsonObject.GetNumber("BreakLv");

        if (jsonObject.ContainsKey("AwakeLv"))
            AwakeLv = (int)jsonObject.GetNumber("AwakeLv");

        // if( jsonObject.ContainsKey("Spirit1") ) Spirit1 = (int)jsonObject.GetNumber("Spirit1");
        // if( jsonObject.ContainsKey("Spirit2") ) Spirit2 = (int)jsonObject.GetNumber("Spirit2");
        // if( jsonObject.ContainsKey("Spirit3") ) Spirit3 = (int)jsonObject.GetNumber("Spirit3");
        // if( jsonObject.ContainsKey("Spirit4") ) Spirit4 = (int)jsonObject.GetNumber("Spirit4");

        // if( jsonObject.ContainsKey("Rigging1") ) Rigging1 = (int)jsonObject.GetNumber("Rigging1");
        // if( jsonObject.ContainsKey("Rigging2") ) Rigging2 = (int)jsonObject.GetNumber("Rigging2");
        // if( jsonObject.ContainsKey("Rigging3") ) Rigging3 = (int)jsonObject.GetNumber("Rigging3");
        // if( jsonObject.ContainsKey("Rigging4") ) Rigging4 = (int)jsonObject.GetNumber("Rigging4");
        // if( jsonObject.ContainsKey("Rigging5") ) Rigging5 = (int)jsonObject.GetNumber("Rigging5");
        // if( jsonObject.ContainsKey("Rigging6") ) Rigging6 = (int)jsonObject.GetNumber("Rigging6");
    }
}