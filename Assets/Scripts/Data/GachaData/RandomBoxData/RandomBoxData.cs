using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public enum RewardType
    {
        Character,
        Item
    }
    public class RandomBoxData
    {
        public int Key;
        public int RewardKey;
        public float Probility;
        public int Count;
        public RewardType RewardType;
    }
}