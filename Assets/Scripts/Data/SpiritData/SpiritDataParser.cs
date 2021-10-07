using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

namespace Data
{
    public class SpiritDataParser : PublicDataParseBase<SpiritDataParser, SpiritData>
    {
        protected override void Init()
        {
            string textAsset = Resources.Load("Json/spirit").ToString();
            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("SpiritTemplate");

            data = new SpiritData[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                data[i].Parse( jsonArray[i].Obj );
            }
        }
    }
}