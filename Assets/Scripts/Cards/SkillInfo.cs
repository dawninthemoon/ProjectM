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
    public int damage;
    public int ownerEntityID;
    public SkillType type;

    public SkillInfo(int cost, int damage, SkillType type) {
        this.requireCost = cost;
        this.damage = damage;
        this.type = type;
    }
}