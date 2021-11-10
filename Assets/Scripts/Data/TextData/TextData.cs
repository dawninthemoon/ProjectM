using Boomlagoon.JSON;

namespace Data
{
    [System.Serializable]
    public class TextData : PublicDataBase
    {
        public string key;
        public string korean;
        public string english;

        public override void Parse(JSONObject jsonObject)
        {
            key = jsonObject.GetString("key");
            korean = jsonObject.GetString("korean");
            english = jsonObject.GetString("english");
        }
    }
}