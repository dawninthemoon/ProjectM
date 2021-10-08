using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class ItemDataParser : PublicDataParseBase<ItemDataParser, ItemData>
    {
        public ItemDataParser() {
            assetPath = "Json/item";
            templateName = "ItemTemplate";
        }

        public ItemData GetItemData( int key )
        {
            return Array.Find( data, (x) => x.Key == key );
        }
    }
}