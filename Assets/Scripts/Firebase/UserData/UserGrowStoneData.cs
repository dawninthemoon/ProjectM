using Boomlagoon.JSON;

public class UserGrowStoneData
{
    public static readonly int GROW_STONE_COUNT = 5;
    public static readonly string GROW_STONE_SAVE_KEY = "GrowStone{0}";

    private int[] growStone = new int[GROW_STONE_COUNT];
    public int[] GrowStone
    {
        get { return growStone; }
    }

    public void GetJsonData(JSONObject jsonObject)
    {
        for (int i = 0; i < GROW_STONE_COUNT; ++i)
        {
            string propertyName = string.Format(GROW_STONE_SAVE_KEY, i);
            if (jsonObject.ContainsKey(propertyName))
                growStone[i] = (int)jsonObject.GetNumber(propertyName);
        }
    }

    public void SetJsonData(JSONObject jsonObject)
    {
        for (int i = 0; i < GROW_STONE_COUNT; ++i)
        {
            string propertyName = string.Format(GROW_STONE_SAVE_KEY, i);
            jsonObject.Add(propertyName, growStone[i]);
        }
    }
    public void SetDefaultData()
    {
        growStone = new int[GROW_STONE_COUNT];

        for (int i = 0; i < growStone.Length; ++i)
            growStone[i] = 0;
    }

    public void AddStone( int index, int addCount )
    {
        growStone[index] += addCount;

        if (growStone[index] <= 0)
            growStone[index] = 0;

        FBControl.FirebaseManager.Instance.UserDB.SaveChildrenData(string.Format(GROW_STONE_SAVE_KEY, index),growStone[index]);
    }
}
