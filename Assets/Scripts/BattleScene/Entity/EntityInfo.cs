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
    public string name = null;

    public EntityInfo(int id, int hp, int atk, int heal, int def, string name) {
        this.entityID = id;
        this.maxHP = hp;
        this.atk = atk;
        this.heal = heal;
        this.def = def;
        this.name = name;
    }
}
