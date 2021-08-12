using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityInfo {
    public int entityID;
    public int maxHP;
    public int atk;
    public int heal;
    public int def;

    public EntityInfo(int id, int hp, int atk, int heal, int def) {
        this.entityID = id;
        this.maxHP = hp;
        this.atk = atk;
        this.heal = heal;
        this.def = def;
    }
}
