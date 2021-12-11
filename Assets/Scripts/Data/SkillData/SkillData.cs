using Boomlagoon.JSON;
using UnityEngine;

namespace Data
{
    public struct SkillInfo
    {
        public SkillData SkillData;
        public int CharacterKey;

        public SkillInfo(SkillData skillData, int characterKey)
        {
            SkillData = skillData;
            CharacterKey = characterKey;
        }
    }

    public enum SkillType
    {
        NormalSkill,
        TurnSkill,
        UltimateSkill,
        PassiveSkill
    }

    public enum CastType
    {
        EmemyCast,
        TeamCast,
        NoneCast
    }

    public enum EnemyTargetType
    {
        None,
        Single,
        Multi,
        Combo, // 대상과 대상보다 순서가 1 많거나 적은 몬스터(CastType이 0이어야 함)
        Random
    }

    public enum AllyTargetType
    {
        None,
        Single,
        Multi,
        Combo,
        Random,
        SelfAndSingle
    }

    public class SkillData : PublicDataBase
    {
        public int Key;
        public string Name;
        public string IconKey;
        public SkillType SkillType;
        public CastType CastType;
        public int Cost;
        public EnemyTargetType EnemyTargetType;
        public int EnemyTargetCount;
        public AllyTargetType AllyTargetType;
        public int AllyTargetCount;
        public int AttackRatio;
        public int HealRatio;
        public int Shield;
        public bool Shield_ValueType;
        public int Draw;
        public bool Draw_DeckType;
        public bool Draw_TargetType;
        public int GainCost;
        public bool Excluded;
        public bool Disposable;
        public int[] AddCard;
        public int[] Shuffle_Deck;

        public override void Parse(JSONObject jsonObject)
        {
            Key = (int)jsonObject.GetNumber("Key");
            Name = jsonObject.GetString("Name");
            IconKey = jsonObject.GetString("IconKey");
            SkillType = (SkillType)(int)jsonObject.GetNumber("SkillType");//(SkillType)System.Enum.Parse( typeof( SkillType ), jsonObject.GetString( "SkillType" ) );
            CastType = (CastType)(int)jsonObject.GetNumber("CastType");
            Cost = (int)jsonObject.GetNumber("Cost");
            EnemyTargetType = (EnemyTargetType)(int)jsonObject.GetNumber("EnemyTargetType");
            EnemyTargetCount = (int)jsonObject.GetNumber("EnemyTargetCount");
            AllyTargetType = (AllyTargetType)(int)jsonObject.GetNumber("AllyTargetType");
            AllyTargetCount = (int)jsonObject.GetNumber("AllyTargetCount");
            AttackRatio = (int)jsonObject.GetNumber("AttackRatio");
            HealRatio = (int)jsonObject.GetNumber("HealRatio");
            Shield = (int)jsonObject.GetNumber("Shield");
            Shield_ValueType = (int)jsonObject.GetNumber("Shield_ValueType") < Mathf.Epsilon;
            Draw = (int)jsonObject.GetNumber("Draw");
            Draw_DeckType = jsonObject.GetNumber("Draw_DeckType") < Mathf.Epsilon;
            Draw_TargetType = jsonObject.GetNumber("Draw_TargetType") < Mathf.Epsilon;
            GainCost = (int)jsonObject.GetNumber("GainCost");
            Excluded = jsonObject.GetNumber("Excluded") < Mathf.Epsilon;
            Disposable = jsonObject.GetNumber("Disposable") < Mathf.Epsilon;
            AddCard = GetID(jsonObject.GetString("AddCard"));
            Shuffle_Deck = GetID(jsonObject.GetString("Shuffle_Deck"));
        }

        private int[] GetID(string origin)
        {
            if (origin == null) return null;
            origin = origin.TrimStart('{');
            origin = origin.TrimEnd('}');
            string[] keys = origin.Split(',');
            int[] nKeys = new int[keys.Length];
            for (int i = 0; i < keys.Length; ++i)
            {
                nKeys[i] = int.Parse(keys[i]);
            }
            return nKeys;
        }
    }
}