using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager> {
    static readonly string PrefabPath = "Prefabs/";
    static readonly string SkillPrefabName = "SkillPrefab";
    static readonly string EntityPrefabName = "EntityPrefab";
    Dictionary<string, GameObject> _cachedPrefabs;
    public ResourceManager() {
        _cachedPrefabs = new Dictionary<string, GameObject>();
    }

    public Skill GetSkillPrefab() {
        string path = PrefabPath + SkillPrefabName;
        var skill = LoadPrefab(path).GetComponent<Skill>();
        return skill;
    }

    public BattleEntity GetEntityPrefab() {
        string path = PrefabPath + EntityPrefabName;
        var entity = LoadPrefab(path).GetComponent<BattleEntity>();
        return entity;
    }

    GameObject LoadPrefab(string path) {
        GameObject gameObject = null;
        if (!_cachedPrefabs.TryGetValue(path, out gameObject)) {
            gameObject = Resources.Load<GameObject>(path);
            _cachedPrefabs.Add(path, gameObject);
        }
        return gameObject;
    }

    public Sprite GetSprite(string path) {
        Sprite sprite = null;
        sprite = Resources.Load<Sprite>(path);
        return sprite;
    }
}
