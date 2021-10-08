using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;

namespace Data
{
    public class RandomBoxDataParser : PublicDataParseBase<RandomBoxDataParser, RandomBoxData>
    {
        public RandomBoxDataParser() {
            assetPath = "Json/randombox";
            templateName = "RandomBoxTemplate";
        }

        public RandomBoxData GetRandomBoxData( int index )
        {
            return Array.Find( data, (x)=> x.Key == index );
        } 

        public RandomBoxData GetRandomBoxResult( int index )
        {
            RandomBoxData[] dataArray = Array.FindAll( data, (x)=> x.Key == index );

            float allWeight = 0;
            float calculateWeight = 0;

            for( int i = 0; i < dataArray.Length; ++i )
                allWeight += dataArray[i].Probility;

            float randomValue = UnityEngine.Random.Range( 0, allWeight );

            calculateWeight = 0;

            for( int i = 0; i < dataArray.Length; ++i )
            {
                if( calculateWeight >= randomValue && calculateWeight < randomValue + dataArray[i].Probility )
                    return dataArray[i];

                calculateWeight += dataArray[i].Probility;
            }

            return null;
        }

        public RandomBoxData[] GetRandomBoxResultArray( int index, int count )
        {
            RandomBoxData[] result = new RandomBoxData[count];
            
            RandomBoxData[] dataArray = Array.FindAll( data, (x)=> x.Key == index );

            float allWeight = 0;
            float calculateWeight = 0;

            for( int i = 0; i < dataArray.Length; ++i )
                allWeight += dataArray[i].Probility;

            for( int k = 0; k < count; ++k )
            {
                float randomValue = UnityEngine.Random.Range( 0, (int)allWeight + 1 );

                calculateWeight = 0;

                Debug.Log("RNDOM : " + randomValue );

                for( int i = 0; i < dataArray.Length; ++i )
                {
                    if( calculateWeight <= randomValue && calculateWeight + dataArray[i].Probility > randomValue )
                    {
                        result[k] = dataArray[i];
                        break;
                    }
                    else
                    {
                        calculateWeight += dataArray[i].Probility;
                    }
                }
            }

            return result;
        }
    }
}