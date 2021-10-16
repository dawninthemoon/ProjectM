using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

namespace Data
{
    public struct SkillInfo {
        public SkillData SkillData;
        public int CharacterKey;

        public SkillInfo(SkillData skillData, int characterKey) {
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

    public enum AttackType
    {
        None,
        SingleAttack,
        MultiAttack,
        ComboAttack, // 대상과 대상보다 순서가 1 많거나 적은 몬스터(CastType이 0이어야 함)
        RandomAttack
    }
    public enum HealType
    {
        None,
        SingleHeal,
        MultiHeal,
        ComboHeal,
        RandomHeal
    }

    public class SkillData : PublicDataBase
    {
        public int Key;
        public string Name;
        public string IconKey;
        public SkillType SkillType;
        public int Cost;
        public CastType CastType;
        public AttackType AttackType;
        public int AttackTypeValue;
        public int AttackRatio;
        public HealType HealType;
        public int HealTypeValue;
        public int HealRatio;
    
        public override void Parse( JSONObject jsonObject )
        {
            Key = (int)jsonObject.GetNumber( "Key" );
            Name = jsonObject.GetString( "Name" );
            IconKey = jsonObject.GetString( "IconKey" );
            SkillType = (SkillType)(int)jsonObject.GetNumber("SkillType");//(SkillType)System.Enum.Parse( typeof( SkillType ), jsonObject.GetString( "SkillType" ) );
            Cost = (int)jsonObject.GetNumber( "Cost" );
            CastType = (CastType)(int)jsonObject.GetNumber("CastType");//(CastType)System.Enum.Parse( typeof( CastType ), jsonObject.GetString( "CastType" ) );
            AttackType = (AttackType)(int)jsonObject.GetNumber("AttackType");//(AttackType)System.Enum.Parse( typeof( AttackType ), jsonObject.GetString( "AttackType" ) );
            AttackTypeValue = (int)jsonObject.GetNumber( "AttackTypeValue" );
            AttackRatio = (int)jsonObject.GetNumber( "AttackRatio" );
            HealType = (HealType)(int)jsonObject.GetNumber("HealType");//(HealType)System.Enum.Parse( typeof( HealType ), jsonObject.GetString( "HealType" ) );
            HealTypeValue = (int)jsonObject.GetNumber( "HealTypeValue" );
            HealRatio = (int)jsonObject.GetNumber( "HealRatio" );
        }
    }
}
