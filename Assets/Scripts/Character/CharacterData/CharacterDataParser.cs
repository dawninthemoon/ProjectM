using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Boomlagoon.JSON;

namespace Data
{
    public class CharacterDataParser
    {

        public static Character Parse( JSONObject jsonObj  )
        {
            Character character = new Character();

            character.Key = jsonObj.GetString("Key");
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
    }
}