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
        private string subName;
        public string SubName
        {
            get{ return subName; }
        }
        private Character.EClassType eClassType;
        private int grade;
        public int Grade
        {
            get{ return grade; }
        }
        private string skillCard1Key;
        private string passiveSkillKey;
        private string iconName;
        public string IconName
        {
            get{ return iconName; }
        }

        public static int GetRequestSoulToStar( int star )
        {
            switch( star )
            {
                case 0:
                    return 20;
                case 1:
                    return 20;
                case 2:
                    return 50;
                case 3:
                    return 80;
                case 4:
                    return 100;
            }

            return 0;
        }

        public override void Parse(JSONObject jsonObject)
        {
            key = (int)jsonObject.GetNumber("Key");
            name = jsonObject.GetString("Name");
            subName = jsonObject.GetString("SubName");
            eClassType = (Character.EClassType)System.Enum.Parse( typeof( Character.EClassType), jsonObject.GetString("Class"));
            grade = (int)jsonObject.GetNumber("Grade");
            skillCard1Key = jsonObject.GetString("SkillCard1Key");
            passiveSkillKey = jsonObject.GetString("PassiveSkillKey");
            iconName = jsonObject.GetString("IconName");
        }
    }
}