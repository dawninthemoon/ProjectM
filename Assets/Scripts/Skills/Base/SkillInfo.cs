using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType {
    ENEMY_TARGET,
    ENEMY_RANDOM,
    FRIENDLY_TARGET,
    FRIENDLY_RANDOM,
    SELF,
}

[System.Serializable]
public class SkillInfo {
    public int requireCost;
    public int value;
    public int ownerEntityID;
    public SkillType type;
    public int numOfTargets;
    public SOSkillEffectBase skillEffect;

    public SkillInfo(int cost, int value, SkillType type, SOSkillEffectBase effect) {
        this.requireCost = cost;
        this.value = value;
        this.type = type;
        this.skillEffect = effect;
    }
}