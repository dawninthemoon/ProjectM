using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class GachaDataParser : PublicDataParseBase<GachaDataParser, GachaData>
    {
        protected override void Init()
        {
            string textAsset = Resources.Load("Json/gacha").ToString();
            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("GachaTemplate");

            data = new GachaData[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                data[i].Parse( jsonArray[i].Obj );
            }
        }

        public GachaData GetGachaData( int gachaIndex )
        {
            return Array.Find( data, (x) => x.Key == gachaIndex );
        }
    }
}