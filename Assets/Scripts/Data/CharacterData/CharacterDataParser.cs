using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Boomlagoon.JSON;

namespace Data
{
    public class CharacterDataParser : PublicDataParseBase<CharacterDataParser, Character>
    {
        protected override void Init()
        {
            string textAsset = Resources.Load("Json/character").ToString();

            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("CharacterTemplate");

            data = new Character[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                data[i].Parse( jsonArray[i].Obj );
            }
        }


        public Character GetCharacter( int key )
        {
            return Array.Find( data, (x) => x.Key == key );
        }
    }
}