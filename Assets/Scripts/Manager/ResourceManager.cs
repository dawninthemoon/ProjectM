using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager> {
    static readonly string PrefabPath = "Prefabs/";

    public Skill GetCardPrefab() {
        string path = PrefabPath + "SkillPrefab";
        var card = Resources.Load<Skill>(path);
        return card;
    }
}
