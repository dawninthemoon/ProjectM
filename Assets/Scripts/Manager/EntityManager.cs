using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : SingletonWithMonoBehaviour<EntityManager> {
    Entity[] _friendlyEntities;
    Entity[] _enemyEntities;
    protected override void Awake() {
        base.Awake();

        _friendlyEntities = GameObject.Find("FriendlyUnits").GetComponentsInChildren<Entity>();
        _enemyEntities = GameObject.Find("EnemyUnits").GetComponentsInChildren<Entity>();
    }
}
