using System;

namespace Data
{
    [Serializable]
    public partial class SkillGameData : GameData<SkillGameData>
    {
        public int key { get; set; }

        public string nameKey { get; set; }
        public string name => Translator.Get(nameKey);

        public string iconKey { get; set; }

        public SkillType skillType { get; set; }

        public int cost { get; set; }

        public CastType castType { get; set; }

        public EnemyTargetType enemyTargetType { get; set; }
        public int enemyTargetCount { get; set; }

        public AllyTargetType allyTargetType { get; set; }
        public int allyTargetCount { get; set; }

        public int attackRatio { get; set; }
        public int healRatio { get; set; }

        public int shield { get; set; }
        public bool shield_ValueType { get; set; }

        public int draw { get; set; }
        public bool draw_DeckType { get; set; }
        public bool draw_TargetType { get; set; }

        public int gainCost { get; set; }

        public bool excluded { get; set; }

        public bool disposable { get; set; }

        public int[] addCard { get; set; }

        public int[] shuffle_Deck { get; set; }

        public int GetKey()
        {
            return key;
        }
    }
}