using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class RandomBoxDataParser
    {
        private static RandomBoxData[] randomBoxData = null;
        public static RandomBoxData[] RandomBoxData
        {
            get
            {
                Init();
                return randomBoxData;
            }
        }
        
        public static void Init()
        {
            string textAsset = Resources.Load("Json/randombox").ToString();

            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("RandomBoxTemplate");

            randomBoxData = new RandomBoxData[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                randomBoxData[i] = Parse( jsonArray[i].Obj );
            }
        }

        public static RandomBoxData GetRandomBoxData( int index )
        {
            Init();
            return Array.Find( randomBoxData, (x)=> x.Key == index );
        } 

        public static int GetRandomBoxResult( int index )
        {
            Init();
            RandomBoxData[] dataArray = Array.FindAll( randomBoxData, (x)=> x.Key == index );

            float allWeight = 0;

            for( int i = 0; i < dataArray.Length; ++i )
                allWeight += dataArray[i].Probility;

            float randomValue = UnityEngine.Random.Range( 0, allWeight );

            allWeight = 0;

            for( int i = 0; i < dataArray.Length; ++i )
            {
                if( allWeight >= randomValue && allWeight < randomValue + dataArray[i].Probility )
                    return dataArray[i].RewardKey;

                allWeight += dataArray[i].Probility;
            }

            return 0;
        }

        public static RandomBoxData Parse( JSONObject jsonObj  )
        {
            RandomBoxData randomBoxData = new RandomBoxData();

            randomBoxData.Key = (int)jsonObj.GetNumber("Key");
            randomBoxData.RewardKey = (int)jsonObj.GetNumber("RewardKey");
            randomBoxData.Probility = (float)jsonObj.GetNumber("Probility");

            return randomBoxData;
        }
    }
}