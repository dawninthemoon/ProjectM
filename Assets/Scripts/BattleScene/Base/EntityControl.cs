using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;

public struct SharedStat {
    public int Critical;
    public int CriticalDamage;
    public int AttackPower;

    public SharedStat(Data.MonsterStat stat) {
        Critical = stat.Critical;
        CriticalDamage = stat.CriticalDamage;
        AttackPower = stat.AttackPower;
    }

    public SharedStat(Data.CharacterStat stat) {
        Critical = stat.Critical;
        CriticalDamage = stat.CriticalDamage;
        AttackPower = stat.AttackPower;
    }
}

public abstract class EntityControl : MonoBehaviour {
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected Vector3[] _initialPosition = null;
    protected float[] _fillAmounts;
    protected List<BattleEntity> _currentEntities;
    protected string _entityPrefabPath;

    public abstract void Initialize();

    public void Progress() {
        for (int i = 0; i < _currentEntities.Count; ++i) {
            if (_currentEntities[i].CanRemoveEntity) {
                StartCoroutine(RemoveEntity(_currentEntities[i]));
                --i;
                continue;
            }
            _currentEntities[i].Progress();
        }
    }

    public void LateProgress() {
        for (int i = 0; i < _currentEntities.Count; ++i) {
            _currentEntities[i].LateProgress();
        }
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i < _currentEntities.Count; ++i) {
            _fillAmounts[i] = _currentEntities[i].GetHPPercent(); 
        }
        return _fillAmounts;
    }

    public BattleEntity GetSelectedEntity(Vector3 touchPos) {
        BattleEntity target = null;
        int entityCounts = _currentEntities.Count;

        RaycastHit raycastHit;
        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(screenRay, out raycastHit, 100f, _layerMask)) {
            for (int i = 0; i < entityCounts; ++i) {
                if (_currentEntities[i].gameObject == raycastHit.collider.gameObject) {
                    target = _currentEntities[i];
                }
            }
        }
        return target;
    }

    protected void DoAttack(BattleEntity selectedTarget, EntityControl other, Data.SkillInfo skillInfo, SharedStat stat, BattleEntity caster) {
        var data = skillInfo.SkillData;
        float criticalDamage = (Random.Range(0f, 1f) < MathUtils.GetPerTenThousand(stat.Critical)) ? MathUtils.GetPerTenThousand(stat.CriticalDamage) : 0f;
        float baseDamage = stat.AttackPower * (1f + criticalDamage) * MathUtils.GetPercent(data.AttackRatio);
        int targetCounts = data.EnemyTargetCount;
        int finalDamage = 0;
        List<BattleEntity> enemyList;

        switch (data.EnemyTargetType) {
        case Data.EnemyTargetType.Single:
            finalDamage = Mathf.FloorToInt(baseDamage / selectedTarget.GetFinalDefence());
            AttackTarget(selectedTarget, finalDamage);
            break;
        case Data.EnemyTargetType.Combo:
            enemyList = other.GetEntityByOrder(targetCounts, selectedTarget);
            foreach (var enemy in enemyList) {
                finalDamage = Mathf.FloorToInt(baseDamage / enemy.GetFinalDefence());
                AttackTarget(enemy, finalDamage);
            }
            break;
        case Data.EnemyTargetType.Multi:
            enemyList = other.GetAllEntities();
            foreach (var enemy in enemyList) {
                finalDamage = Mathf.FloorToInt(baseDamage / enemy.GetFinalDefence());
                AttackTarget(enemy, finalDamage);
            }
            break;
        case Data.EnemyTargetType.Random:
            enemyList = other.GetRandomEntities(targetCounts);
            foreach (var enemy in enemyList) {
                finalDamage = Mathf.FloorToInt(baseDamage / enemy.GetFinalDefence());
                AttackTarget(enemy, finalDamage);
            }
            break;
        }

        void AttackTarget(BattleEntity entity, int amount) {
            entity.DecreaseHP(amount);
            entity.MoveForward(0.2f);
        }

        if (data.EnemyTargetType != Data.EnemyTargetType.None) {
            caster.MoveForward(1f);
        }
    }

    protected void DoHeal(BattleEntity selectedTarget, Data.SkillInfo skillInfo, SharedStat stat) {
        var data = skillInfo.SkillData;
        int targetCounts = skillInfo.SkillData.AllyTargetCount;
        int finalHeal = (int)MathUtils.GetPerTenThousand(data.HealRatio) * stat.AttackPower;
        List<BattleEntity> entityList;

        switch (skillInfo.SkillData.AllyTargetType) {
        case Data.AllyTargetType.Single:
            HealTarget(selectedTarget, finalHeal);
            break;
        case Data.AllyTargetType.Combo:
            entityList = GetEntityByOrder(targetCounts, selectedTarget);
            foreach (var entity in entityList) {
                HealTarget(entity, finalHeal);
            }
            break;
        case Data.AllyTargetType.Multi:
            entityList = GetAllEntities();
            foreach (var entity in entityList) {
                HealTarget(entity, finalHeal);
            }
            break;
        case Data.AllyTargetType.Random:
            entityList = GetRandomEntities(targetCounts);
            foreach (var entity in entityList) {
                HealTarget(entity, finalHeal);
            }
            break;
        case Data.AllyTargetType.SelfAndSingle:
            BattleEntity self = FindEntityByKey(skillInfo.CharacterKey);
            HealTarget(selectedTarget, finalHeal);
            HealTarget(self, finalHeal);
            break;
        }

        void HealTarget(BattleEntity target, int amount) {
            target.IncreaseHP(amount);
        }
    }

    public List<BattleEntity> GetEntityByOrder(int targetCounts, BattleEntity selectedEntity = null) {
        int numOfEntites = _currentEntities.Count;
        int startIndex = 0;
        List<BattleEntity> entityList = new List<BattleEntity>();

        if (selectedEntity) {
            entityList.Add(selectedEntity as BattleEntity);
        }
        
        for (int i = 0; i < numOfEntites; ++i) {
            if (selectedEntity == _currentEntities[i]) {
                startIndex = (i + 1) % numOfEntites;
                break;
            }
        }

        for (int i = startIndex; entityList.Count == targetCounts; i = (i + 1) % numOfEntites) {
            if (_currentEntities[i] == selectedEntity) break;
            entityList.Add(_currentEntities[i]);
        }

        return entityList;
    }
    public List<BattleEntity> GetAllEntities() {
        return _currentEntities.ToList();
    }
    public List<BattleEntity> GetRandomEntities(int targetCounts) {
        int numOfEntities = _currentEntities.Count;
        var entityList = _currentEntities.ToList();

        if (targetCounts < numOfEntities) {
            int diff = numOfEntities - targetCounts;
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, entityList.Count);
                entityList.RemoveAt(removeIndex);
            }
        }
        return entityList;
    }
    private IEnumerator RemoveEntity(BattleEntity entity) {
        entity.CanRemoveEntity = false;
        entity.ChangeAnimationState("Dead");

        while (!entity.IsAnimationEnd) {
            yield return null;
        }

        _currentEntities.Remove(entity);
        entity.gameObject.SetActive(false);
    }

    public BattleEntity FindEntityByKey(int key) {
        BattleEntity requestedEntity = null;;
        foreach (BattleEntity entity in _currentEntities) {
            if (entity.KeyEquals(key)) {
                requestedEntity = entity;
                break;
            }
        }
        return requestedEntity;
    }
}