using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Boomlagoon.JSON;

namespace Data
{
    public class CharacterDataParser
    {
        private static Character[] characterData = null;
        public static Character[] CharacterData
        {
            get
            {
                Init();
                return characterData;
            }
        }
        
        public static void Init()
        {
            string textAsset = Resources.Load("Json/character").ToString();

            JSONObject jsonObject = JSONObject.Parse(textAsset);

            JSONArray jsonArray = jsonObject.GetArray("CharacterTemplate");

            characterData = new Character[jsonArray.Length];

            for( int i = 0; i < jsonArray.Length; ++i )
            {
                characterData[i] = Parse( jsonArray[i].Obj );
            }
        }

        public static Character Parse( JSONObject jsonObj  )
        {
            Character character = new Character();

            character.Key = (int)jsonObj.GetNumber("Key");
            character.Name = jsonObj.GetString("Name");
            character.SubName = jsonObj.GetString("SubName");
            character.ClassType = (Character.EClassType)Enum.Parse(typeof(Character.EClassType), jsonObj.GetString("Class"));
            character.GenderType = (Character.EGenderType)Enum.Parse(typeof(Character.EGenderType), jsonObj.GetString("Gender"));
            character.Grade = (int)jsonObj.GetNumber("Grade");
            character.Rarity = jsonObj.GetBoolean("Rarity");
            character.SkillCard1Key = jsonObj.GetString("SkillCard1Key");
            character.SkillCard2Key = jsonObj.GetString("SkillCard2Key");
            character.SpecialCardKey = jsonObj.GetString("SpecialCardKey");
            character.TurnSkillKey = jsonObj.GetString("TurnSkillKey");
            character.PassiveSkillKey = jsonObj.GetString("PassiveSkillKey");

            return character;
        }

        public static Character GetCharacter( int key )
        {
            Init();

            return Array.Find( characterData, (x) => x.Key == key );
        }
    }
}