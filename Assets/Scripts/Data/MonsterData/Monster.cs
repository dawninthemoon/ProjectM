using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

namespace Data {
    public class MonsterStat : PublicDataBase {
        public int Key;
        public int AttackPower;
        public int BaseHP;
        public int DefencePower;
        public int Critical;
        public int CriticalDamage;
        public int Shield;

        public override void Parse(JSONObject jsonObj) {
            Key = (int)jsonObj.GetNumber("Key");
            AttackPower = (int)jsonObj.GetNumber("AttackPower");
            BaseHP = (int)jsonObj.GetNumber("BaseHP");
            DefencePower = (int)jsonObj.GetNumber("DefencePower");
            Critical = (int)jsonObj.GetNumber("Critical");
            CriticalDamage = (int)jsonObj.GetNumber("CriticalDamage");
            Shield = (int)jsonObj.GetNumber("Shield");
        }
    }

    public class Monster : PublicDataBase{
        public enum EGradeType {
            Normal,
            Elite,
            Boss
        }

        public int Key;
        public string Name;
        public EGradeType Grade;
        public int Level;
        public string MonsterPrefab;
        public string[] Skill;
        public string StatOverrideID;

        public override void Parse(JSONObject jsonObj) {
            Key = (int)jsonObj.GetNumber("Key");
            Name = jsonObj.GetString("Name");
            Grade = (EGradeType)System.Enum.Parse(typeof(EGradeType), jsonObj.GetString("Grade"));
            Level = (int)jsonObj.GetNumber("Level");
            MonsterPrefab = jsonObj.GetString("MonsterPrefab");
            StatOverrideID = jsonObj.GetString("StatOverrideID");
        }
    }
}
