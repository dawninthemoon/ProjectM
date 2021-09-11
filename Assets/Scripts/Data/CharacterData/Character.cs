using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Character
    {
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
    }
}