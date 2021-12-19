using System;

namespace Data
{
    [Serializable]
    public partial class CharacterStatGameData : GameData<CharacterStatGameData>
    {
        public int key { get; set; }

        public int grade { get; set; }

        public int level { get; set; }

        public int attackPower { get; set; }

        public int baseHP { get; set; }

        public int defencePower { get; set; }

        public int critical { get; set; }
        public int criticalDamage { get; set; }

        public int maxDraw { get; set; }
        public int maxCost { get; set; }

        public int GetKey()
        {
            return key;
        }
    }
}