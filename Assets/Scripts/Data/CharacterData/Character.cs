using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

namespace Data
{
    public class Character : PublicDataBase
    {
        public class ChracterStat {
            public int AttackPower;
            public int BaseHP;
            public int CurrentHP;
            public int DefencePower;
            public int Critical;
            public int CriticalDamage;
            public int Shield;
        }

        public enum EClassType
        {
            None,
            Mage,
            Witch,
            BattleMage,
            MagicalEngineer,
            MagicGirl
        }

        public enum EGenderType
        {
            Asexual,
            Male,
            Female
        }

        public int Key;
        public string Name;
        public string SubName;
        public EClassType ClassType;
        public EGenderType GenderType;
        public int Grade;
        public bool Rarity;
        public string SkillCard1Key;
        public string SkillCard2Key;
        public string SpecialCardKey;
        public string TurnSkillKey;
        public string PassiveSkillKey;

        public override void Parse( JSONObject jsonObj  )
        {
            Key = (int)jsonObj.GetNumber("Key");
            Name = jsonObj.GetString("Name");
            SubName = jsonObj.GetString("SubName");
            ClassType = (Character.EClassType)System.Enum.Parse(typeof(Character.EClassType), jsonObj.GetString("Class"));
            GenderType = (Character.EGenderType)System.Enum.Parse(typeof(Character.EGenderType), jsonObj.GetString("Gender"));
            Grade = (int)jsonObj.GetNumber("Grade");
            Rarity = jsonObj.GetBoolean("Rarity");
            SkillCard1Key = jsonObj.GetString("SkillCard1Key");
            SkillCard2Key = jsonObj.GetString("SkillCard2Key");
            SpecialCardKey = jsonObj.GetString("SpecialCardKey");
            TurnSkillKey = jsonObj.GetString("TurnSkillKey");
            PassiveSkillKey = jsonObj.GetString("PassiveSkillKey");
        }
    }
}