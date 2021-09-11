using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class ItemDataParser
    {
        private static ItemData[] itemData = null;
        public static ItemData[] ItemData
        {
            get
            { 
                if( itemData == null )
                    Init();

                return itemData;
            }
        }   

        public static void Init()
        {
            string textAsset = Resources.Load("Json/item").ToString();

            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("ItemTemplate");

            itemData = new ItemData[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                itemData[i] = new ItemData();
                itemData[i].Parse( jsonArray[i].Obj );
            }
        }

        public static ItemData GetItemData( int key )
        {
            Init();
            
            return Array.Find( itemData, (x) => x.Key == key );
        }
    }
}