using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private static readonly string SpritePath = "Sprites/";
    private static readonly string PrefabPath = "Prefabs/";
    private static readonly string SkillPrefabName = "SkillPrefab";
    private Dictionary<string, GameObject> _cachedPrefabs;

    public ResourceManager()
    {
        _cachedPrefabs = new Dictionary<string, GameObject>();
    }

    public Skill GetSkillPrefab()
    {
        string path = PrefabPath + SkillPrefabName;
        var skill = LoadPrefab(path).GetComponent<Skill>();
        return skill;
    }

    public BattleEntity GetEntityPrefab(string name)
    {
        string path = PrefabPath + name;
        var entity = LoadPrefab(path).GetComponent<BattleEntity>();
        return entity;
    }

    private GameObject LoadPrefab(string path)
    {
        GameObject gameObject = null;
        if (!_cachedPrefabs.TryGetValue(path, out gameObject))
        {
            gameObject = Resources.Load<GameObject>(path);
            _cachedPrefabs.Add(path, gameObject);
        }
        return gameObject;
    }

    public Sprite GetSprite(string path)
    {
        Sprite sprite = null;
        sprite = Resources.Load<Sprite>(SpritePath + path);
        return sprite;
    }
}