using System;

namespace Data
{
    [Serializable]
    public partial class RandomBoxGameData : GameData<RandomBoxGameData>
    {
        public struct RandomBoxItemDataSet
        {
            public RewardType rewardType { get; set; }

            public int rewardKey { get; set; }

            public int count { get; set; }

            public float probility { get; set; }
        }

        public int key { get; set; }

        public RandomBoxItemDataSet[] items { get; set; }

        public int GetKey()
        {
            return key;
        }
    }
}