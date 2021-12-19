using System;

namespace Data
{
    [Serializable]
    public partial class SpiritExpGameData : GameData<SpiritExpGameData>
    {
        public struct SpiritExpDataSet
        {
            public int level { get; set; }
            public int maxExp { get; set; }
        }

        public int grade { get; set; }

        public SpiritExpDataSet exps { get; set; }

        public int GetKey()
        {
            return grade;
        }
    }
}