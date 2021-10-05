using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class SkillDataParser : MonoBehaviour
    {
        private static SkillData[] skillData = null;
        public static SkillData[] SkillData
        {
            get
            { 
                if( skillData == null )
                    Init();

                return skillData;
            }
        }   

        public static void Init()
        {
            string textAsset = Resources.Load("Json/skill").ToString();

            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("SkillTemplate");

            skillData = new SkillData[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                skillData[i] = new SkillData();
                skillData[i].Parse( jsonArray[i].Obj );
            }
        }

        public static SkillData GetSkillData( int key )
        {
            Init();
            
            return Array.Find( skillData, (x) => x.Key == key );
        }
    }
}