using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using UnityEngine;

namespace Data
{
    public class SpiritData : PublicDataBase
    {
        private int key;
        public int Key
        {
            get{ return key; }
        }
        private string name;
        public string Name
        {
            get{ return name; }
        }
        private Character.EClassType eClassType;
        private int grade;
        private string skillCard1Key;
        private string passiveSkillKey;
        private string iconName;
        public string IconName
        {
            get{ return iconName; }
        }

        public override void Parse(JSONObject jsonObject)
        {
            key = (int)jsonObject.GetNumber("Key");
            name = jsonObject.GetString("Name");
            eClassType = (Character.EClassType)System.Enum.Parse( typeof( Character.EClassType), jsonObject.GetString("Class"));
            grade = (int)jsonObject.GetNumber("Grade");
            skillCard1Key = jsonObject.GetString("SkillCard1Key");
            passiveSkillKey = jsonObject.GetString("PassiveSkillKey");
            iconName = jsonObject.GetString("IconName");
        }
    }
}