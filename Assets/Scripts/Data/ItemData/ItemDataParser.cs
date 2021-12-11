using System;

namespace Data
{
    public class ItemDataParser : PublicDataParseBase<ItemDataParser, ItemData>
    {
        public ItemDataParser()
        {
            assetPath = "Json/item";
            templateName = "ItemTemplate";
        }

        public ItemData GetItemData(int key)
        {
            return Array.Find(data, (x) => x.Key == key);
        }
    }
}