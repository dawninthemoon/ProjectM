using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : SingletonWithMonoBehaviour<EntityManager> {
    BattleEntity[] _friendlyEntities;
    BattleEntity[] _enemyEntities;
    protected override void Awake() {
        base.Awake();

        _friendlyEntities = GameObject.Find("FriendlyUnits").GetComponentsInChildren<BattleEntity>();
        _enemyEntities = GameObject.Find("EnemyUnits").GetComponentsInChildren<BattleEntity>();
    }
}
