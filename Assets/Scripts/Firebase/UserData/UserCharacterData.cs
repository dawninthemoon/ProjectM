using Boomlagoon.JSON;

public class UserCharacterData
{
    public int Index;
    public int Lv;
    public int BreakLv; //돌파 레벨
    public int AwakeLv; //각성 레벨

    public UserCharacterData()
    {
        
    }
    public UserCharacterData( int index )
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

        return jsonObject;
    }

    public void SetJsonObject( JSONObject jsonObject )
    {
        if( jsonObject.ContainsKey("Index") )
            Lv = (int)jsonObject.GetNumber("Index");
            
        if( jsonObject.ContainsKey("Lv") )
            Lv = (int)jsonObject.GetNumber("Lv");
            
        if( jsonObject.ContainsKey("BreakLv") )
            BreakLv = (int)jsonObject.GetNumber("BreakLv");
            
        if( jsonObject.ContainsKey("AwakeLv") )
            AwakeLv = (int)jsonObject.GetNumber("AwakeLv");
    }
}
