using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class GachaDataParser
    {
        private static GachaData[] gachaData = null;
        public static GachaData[] GachaData
        {
            get
            {
                Init();
                return gachaData;
            }
        }
        
        public static void Init()
        {
            string textAsset = Resources.Load("Json/gacha").ToString();
            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("GachaTemplate");

            gachaData = new GachaData[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                gachaData[i] = Parse( jsonArray[i].Obj );
            }
        }

        public static GachaData Parse( JSONObject jsonObj  )
        {
            GachaData gachaData = new GachaData();

            gachaData.Key = (int)jsonObj.GetNumber("Key");
            gachaData.Name = jsonObj.GetString("Name");
            gachaData.Sequnce = (int)jsonObj.GetNumber("Sequence");
            gachaData.CashType = (CurrencyType)Enum.Parse(typeof(CurrencyType), jsonObj.GetString("CashType"));
        
            gachaData.CashValue = (int)jsonObj.GetNumber("CashValue");
            gachaData.CouponKey = jsonObj.GetString("CouponKey");
            
            gachaData.CouponMileageStack = (int)jsonObj.GetNumber("CouponMileageStack");
            gachaData.RandomBoxKey = (int)jsonObj.GetNumber("RandomBoxKey");
            if( jsonObj.ContainsKey("AdvantageRandomBoxKey") )
                gachaData.AdvantageRandomBoxKey = (int)jsonObj.GetNumber("AdvantageRandomBoxKey");
            if( jsonObj.ContainsKey("MileageKey") )
                gachaData.MileageKey = (int)jsonObj.GetNumber("MileageKey");
            if( jsonObj.ContainsKey("MileageValue") )
                gachaData.MileageValue = (int)jsonObj.GetNumber("MileageValue");
            if( jsonObj.ContainsKey("AdvantageMileageValue") )
                gachaData.AdvantageMileageValue = (int)jsonObj.GetNumber("AdvantageMileageValue");
            if( jsonObj.ContainsKey("FreeGachaType") )
                gachaData.FreeGachaType = (int)jsonObj.GetNumber("FreeGachaType");
            if( jsonObj.ContainsKey("FreeGachaValue") )
                gachaData.FreeGachaValue = (int)jsonObj.GetNumber("FreeGachaValue");

            return gachaData;
        }

        public static GachaData GetGachaData( int gachaIndex )
        {
            Init();
            return Array.Find( gachaData, (x) => x.Key == gachaIndex );
        }
    }
}