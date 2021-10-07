using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster {
    public enum EGradeType {
        Normal,
        Elite,
        Boss
    }

    public class Monster : MonoBehaviour
    {
        public string Key;
        public string Name;
        public EGradeType Grade;
        public int Level;
        public string MonsterPrefab;
        public string[] Skill;
        public string StatOverrideID;
    }
}
