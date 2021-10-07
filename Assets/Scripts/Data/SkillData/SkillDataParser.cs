using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class SkillDataParser : PublicDataParseBase<SkillDataParser, SkillData>
    {
        protected override void Init()
        {
            string textAsset = Resources.Load("Json/skill").ToString();

            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("SkillTemplate");

            data = new SkillData[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                data[i] = new SkillData();
                data[i].Parse( jsonArray[i].Obj );
            }
        }

        public SkillData GetSkillData( int key )
        {
            return Array.Find( data, (x) => x.Key == key );
        }
    }
}