using Boomlagoon.JSON;

namespace Data
{
    public class RandomBoxData : PublicDataBase
    {
        public int Key;
        public int RewardKey;
        public float Probility;
        public int Count;
        public RewardType RewardType;

        public override void Parse(JSONObject jsonObj)
        {
            Key = (int)jsonObj.GetNumber("Key");
            RewardKey = (int)jsonObj.GetNumber("RewardKey");
            Probility = (float)jsonObj.GetNumber("Probility");
            Count = (int)jsonObj.GetNumber("Count");
            RewardType = (RewardType)System.Enum.Parse(typeof(RewardType), jsonObj.GetString("RewardType"));
        }
    }
}