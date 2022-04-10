using Boomlagoon.JSON;

namespace Data
{
    public class ItemData : PublicDataBase
    {
        public int Key;
        public string Name;
        public string Description;
        public ItemType itemType;
        public int Grade;
        public bool Rarity;
        public string ItemIcon;
        public string BackgroundIcon;

        public override void Parse(JSONObject jsonObject)
        {
            Key = (int)jsonObject.GetNumber("Key");
            Name = jsonObject.GetString("Name");
            Description = jsonObject.GetString("Description");

            Name = jsonObject.GetString("Name");

            Grade = (int)jsonObject.GetNumber("Grade");
            Rarity = jsonObject.GetBoolean("Rarity");

            ItemIcon = jsonObject.GetString("ItemIcon");
            BackgroundIcon = jsonObject.GetString("BackgroundIcon");
        }
    }
}