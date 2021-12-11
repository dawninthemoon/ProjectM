using Boomlagoon.JSON;

namespace Data
{
    public class CharacterStat : PublicDataBase
    {
        public int Key;
        public int Grade;
        public int AttackPower;
        public int BaseHP;
        public int DefencePower;
        public int Critical;
        public int CriticalDamage;
        public int MaxDraw;
        public int MaxCost;

        public override void Parse(JSONObject jsonObject)
        {
            Key = (int)jsonObject.GetNumber("Key");
            Grade = (int)jsonObject.GetNumber("Grade");
            AttackPower = (int)jsonObject.GetNumber("AttackPower");
            BaseHP = (int)jsonObject.GetNumber("BaseHP");
            DefencePower = (int)jsonObject.GetNumber("DefencePower");
            Critical = (int)jsonObject.GetNumber("Critical");
            CriticalDamage = (int)jsonObject.GetNumber("CriticalDamage");
            MaxDraw = (int)jsonObject.GetNumber("MaxDraw");
            MaxCost = (int)jsonObject.GetNumber("MaxCost");
        }
    }

    public class Character : PublicDataBase
    {
        public enum EClassType
        {
            None,
            Guardian,
            Warrior,
            Shooter,
            Mage,
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
        public string[] SkillCard1Key;
        public string SkillCard2Key;
        public string SpecialCardKey;
        public string TurnSkillKey;
        public string PassiveSkillKey;

        public override void Parse(JSONObject jsonObj)
        {
            Key = (int)jsonObj.GetNumber("Key");
            Name = jsonObj.GetString("Name");
            SubName = jsonObj.GetString("SubName");
            ClassType = (Character.EClassType)System.Enum.Parse(typeof(Character.EClassType), jsonObj.GetString("Class"));
            GenderType = (Character.EGenderType)System.Enum.Parse(typeof(Character.EGenderType), jsonObj.GetString("Gender"));
            Grade = (int)jsonObj.GetNumber("Grade");
            SkillCard1Key = GetID(jsonObj.GetString("SkillCard1Key"));
            //SkillCard2Key = jsonObj.GetString("SkillCard2Key");
            SpecialCardKey = jsonObj.GetString("SpecialCardKey");
            TurnSkillKey = jsonObj.GetString("TurnSkillKey");
            PassiveSkillKey = jsonObj.GetString("PassiveSkillKey");
        }

        private string[] GetID(string origin)
        {
            if (origin == null) return null;
            origin = origin.TrimStart('{');
            origin = origin.TrimEnd('}');
            string[] keys = origin.Split(',');
            return keys;
        }
    }
}