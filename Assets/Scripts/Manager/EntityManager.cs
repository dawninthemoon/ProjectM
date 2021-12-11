using UnityEngine;
using Utills;

public class EntityManager : SingletonBehaviour<EntityManager>
{
    private BattleEntity[] _friendlyEntities;
    private BattleEntity[] _enemyEntities;

    protected override void OnAwake()
    {
        _friendlyEntities = GameObject.Find("FriendlyUnits").GetComponentsInChildren<BattleEntity>();
        _enemyEntities = GameObject.Find("EnemyUnits").GetComponentsInChildren<BattleEntity>();
    }
}