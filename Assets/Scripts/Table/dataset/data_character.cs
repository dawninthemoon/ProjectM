using System;

namespace Data
{
    [Serializable]
    public partial class CharacterGameData : GameData<CharacterGameData>
    {
        public int key { get; set; }

        public string nameKey { get; set; }
        public string name => Translator.Get(nameKey);

        public string subNameKey { get; set; }
        public string subName => Translator.Get(subNameKey);

        public Character.EClassType classType { get; set; }
        public Character.EGenderType genderType { get; set; }

        public int grade { get; set; }

        public string[] skillCard1Key { get; set; }
        public string skillCard2Key { get; set; }

        public string specialCardKey { get; set; }

        public string turnSkillKey { get; set; }

        public string passiveSkillKey { get; set; }

        public int GetKey()
        {
            return grade;
        }
    }
}