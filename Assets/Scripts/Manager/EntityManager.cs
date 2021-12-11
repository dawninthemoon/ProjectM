using UnityEngine;

public class EntityManager : SingletonWithMonoBehaviour<EntityManager>
{
    private BattleEntity[] _friendlyEntities;
    private BattleEntity[] _enemyEntities;

    protected override void Awake()
    {
        base.Awake();

        _friendlyEntities = GameObject.Find("FriendlyUnits").GetComponentsInChildren<BattleEntity>();
        _enemyEntities = GameObject.Find("EnemyUnits").GetComponentsInChildren<BattleEntity>();
    }
}