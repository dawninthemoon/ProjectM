using System;

namespace Data
{
    [Serializable]
    public partial class ItemInfoGameData : GameData<ItemInfoGameData>
    {
        public int key { get; set; }

        public string nameKey { get; set; }
        public string name => Translator.Get(nameKey);

        public string descriptionKey { get; set; }
        public string description => Translator.Get(descriptionKey);

        public ItemType type;

        public int grade;

        public bool rarity;

        public string icon;

        public string backgroundIcon;

        public int GetKey()
        {
            return key;
        }
    }
}