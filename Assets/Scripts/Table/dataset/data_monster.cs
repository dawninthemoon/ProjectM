using System;

namespace Data
{
    [Serializable]
    public partial class MonsterGameData : GameData<MonsterGameData>
    {
        public int grade { get; set; }

        public int level { get; set; }

        public int attackPower { get; set; }

        public int baseHP { get; set; }

        public int defencePower { get; set; }

        public int critical { get; set; }
        public int criticalDamage { get; set; }

        public int shield { get; set; }

        public int GetKey()
        {
            return grade;
        }
    }
}