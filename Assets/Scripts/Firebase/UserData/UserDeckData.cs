using Boomlagoon.JSON;
using CodeStage.AntiCheat.ObscuredTypes;

public class UserDeckData
{
    private int[] charIndies = new int[]{ 101, 102, 103 };
    private int[] spiritIndies = new int[9];

    public UserDeckData()
    {
        Load();
    }

    public void SetCharacterIndex( int index, int characterIndex )
    {
        charIndies[index] = characterIndex;
        Save();
    }

    public void SetMainSpiritIndex( int index, int spirit )
    {
        spiritIndies[index] = spirit;
        Save();
    }

    public int GetCharacterIndex( int index )
    {
        return charIndies[index]; 
    }
    public int GetMainSpiritIndex( int index )
    {
        return spiritIndies[index]; 
    }

    public void Save()
    {
        string json = ToJson().ToString();
        ObscuredPrefs.SetString("UserDeckData", json );
        ObscuredPrefs.Save();
    }

    public void Load()
    {
        string json = ObscuredPrefs.GetString("UserDeckData", null );

        if( json == null || json == "" )
        {
            return;
        }

        SetJson( JSONObject.Parse( json ) );
    }

    public JSONObject ToJson()
    {
        JSONObject saveJson = new JSONObject();
        JSONArray arr = new JSONArray();

        for( int i = 0; i < spiritIndies.Length; ++i )
        {
            arr.Add(spiritIndies[i]);
        }
        saveJson.Add( "SpritArr", arr );
        arr = new JSONArray();

        for( int i = 0; i < charIndies.Length; ++i )
            arr.Add(charIndies[i]);
        saveJson.Add( "CharArr", arr );

        return saveJson;
    }

    public void SetJson( JSONObject jsonObject )
    {
        JSONArray arr = jsonObject.GetArray( "SpritArr" );

        for( int i = 0; i < spiritIndies.Length; ++i )
            spiritIndies[i] = (int)arr[i].Number;
            
        arr = jsonObject.GetArray( "CharArr" );

        for( int i = 0; i < charIndies.Length; ++i )
            charIndies[i] = (int)arr[i].Number;
    }
}