using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class ItemDataParser : PublicDataParseBase<ItemDataParser, ItemData>
    {
        protected override void Init()
        {
            string textAsset = Resources.Load("Json/item").ToString();

            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("ItemTemplate");

            data = new ItemData[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                data[i] = new ItemData();
                data[i].Parse( jsonArray[i].Obj );
            }
        }

        public ItemData GetItemData( int key )
        {
            return Array.Find( data, (x) => x.Key == key );
        }
    }
}