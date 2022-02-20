using System;

namespace Data
{
    [Serializable]
    public partial class SpiritGameData : GameData<SpiritGameData>
    {
        public int key { get; set; }

        public string nameKey { get; set; }
        public string name => Translator.Get(nameKey);

        public string subNameKey { get; set; }
        public string subName => Translator.Get(subNameKey);

        public int grade;
        public SpiritStartGradeType startGrade;

        public Character.EClassType classType;

        public int gender { get; set; }
        public string passiveSkillKey { get; set; }

        public string skillCard1Key { get; set; }
        public string skillCard2Key { get; set; }

        public string specialSkillKey { get; set; }

        public string prefabName { get; set; }

        public string iconName { get; set; }

        public int GetKey()
        {
            return key;
        }
    }
}