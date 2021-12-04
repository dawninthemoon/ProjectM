using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;
using DG.Tweening;

public class MonsterControl : MonoBehaviour {
    [SerializeField] private Vector3[] _initialPosition = null;
    [SerializeField] LayerMask _layerMask;
    private List<MonsterEntity> _currentMonsters;
    private float[] _fillAmounts;
    private static readonly string MonsterPreafabPath = "MonsterEntityPrefab/";

    public void Initialize() {
        _fillAmounts = new float[5];

        var stage = Data.BattleStageDataParser.Instance.FindStage("1");
        int[] monsterKeys = stage.Round1_Monster;

        int monsterCounts = monsterKeys.Length;
        _currentMonsters = new List<MonsterEntity>(monsterCounts);

        for (int i = 0; i < monsterCounts; ++i) {
            var monsterData = Data.MonsterDataParser.Instance.GetMonster(monsterKeys[i]);
            var prefab = ResourceManager.GetInstance().GetEntityPrefab(MonsterPreafabPath + monsterData.MonsterPrefab);

            BattleEntity monster = Instantiate(prefab, _initialPosition[i], Quaternion.identity);
            _currentMonsters.Add(monster as MonsterEntity);
            _currentMonsters[i].Initialize(monsterKeys[i], i);
        }
    }

    public void Progress() {
        for (int i = 0; i < _currentMonsters.Count; ++i) {
            if (_currentMonsters[i].CanRemoveEntity) {
                StartCoroutine(RemoveMonster(_currentMonsters[i]));
                --i;
                continue;
            }
            _currentMonsters[i].Progress();
        }
    }

    public void LateProgress() {
        for (int i = 0; i < _currentMonsters.Count; ++i) {
            _currentMonsters[i].Progress();
        }
    }

    public MonsterEntity GetSelectedEnemy(Vector3 touchPos) {
        MonsterEntity target = null;
        int monsterCounts = _currentMonsters.Count;

        RaycastHit raycastHit;
        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(screenRay, out raycastHit, 100f, _layerMask)) 
        {
            for (int i = 0; i < monsterCounts; ++i) {
                if (_currentMonsters[i].gameObject == raycastHit.collider.gameObject) {
                    target = _currentMonsters[i];
                }
            }
        }
        return target;
    }

    public IEnumerator UseSkill(BattleControl battleControl, System.Action uiSetupCallback) {
        foreach (MonsterEntity monster in _currentMonsters) {
            if (battleControl.PlayerCtrl.IsDefeated()) break;
            
            Camera.main.DOOrthoSize(7.4f, 0.3f).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(0.5f);

            Data.SkillData skillData = monster.GetCurrentSkillData();
            if (skillData == null) continue;
            Data.SkillInfo skillInfo = new Data.SkillInfo(skillData, monster.Key);

            int grade = (int)Data.MonsterDataParser.Instance.GetMonster(skillInfo.CharacterKey).Level;
            Data.MonsterStat stat = Data.MonsterStatDataParser.Instance.GetMonsterStat(grade);

            DoAttack(battleControl, skillInfo, stat, monster);
            DoHeal(battleControl, skillInfo, stat);

            string name = Data.MonsterDataParser.Instance.GetMonster(skillInfo.CharacterKey).Name;
            Debug.Log(name + "이 " + skillInfo.SkillData.Name + "을 사용!");
            monster.ChangeAnimationState("Attack");

            uiSetupCallback.Invoke();

            while (!monster.IsAnimationEnd) {
                yield return null;
            }

            monster.ChangeAnimationState("Idle", true);
            monster.SetAnimationDelay(1f);
        }
    }

    private void DoAttack(BattleControl battleControl, Data.SkillInfo skillInfo, Data.MonsterStat stat, MonsterEntity caster) {
        var data = skillInfo.SkillData;
         float criticalDamage = (Random.Range(0f, 1f) < MathUtils.GetPerTenThousand(stat.Critical)) ? MathUtils.GetPerTenThousand(stat.CriticalDamage) : 0f;
        float baseDamage = stat.AttackPower * (1f + criticalDamage) * MathUtils.GetPercent(data.AttackRatio);
        int targetCounts = data.EnemyTargetCount;

        var randomCharacters = battleControl.PlayerCtrl.GetRandomCharacters(1);
        if (randomCharacters.Count == 0) return;
        CharacterEntity selectedTarget = randomCharacters[0];
        int finalDamage;
        List<CharacterEntity> characterList;

        switch (data.EnemyTargetType) {
        case Data.EnemyTargetType.Single:
            finalDamage = Mathf.FloorToInt(baseDamage / selectedTarget.GetFinalDefence());
            AttackTarget(selectedTarget, finalDamage);
            break;
        case Data.EnemyTargetType.Combo:
            characterList = battleControl.PlayerCtrl.GetCharacterByOrder(targetCounts, selectedTarget);
            foreach (var enemy in characterList) {
                finalDamage = Mathf.FloorToInt(baseDamage / enemy.GetFinalDefence());
                AttackTarget(enemy, finalDamage);
            }
            break;
        case Data.EnemyTargetType.Multi:
            characterList = battleControl.PlayerCtrl.GetAllCharacters();
            foreach (var enemy in characterList) {
                finalDamage = Mathf.FloorToInt(baseDamage / enemy.GetFinalDefence());
                AttackTarget(enemy, finalDamage);
            }
            break;
        case Data.EnemyTargetType.Random:
            characterList = battleControl.PlayerCtrl.GetRandomCharacters(targetCounts);
            foreach (var enemy in characterList) {
                finalDamage = Mathf.FloorToInt(baseDamage / enemy.GetFinalDefence());
                AttackTarget(enemy, finalDamage);
            }
            break;
        }

        void AttackTarget(BattleEntity entity, int amount) {
            entity.DecreaseHP(amount);
            if (entity.CurHP <= 0) {
                battleControl.PlayerCtrl.RemoveCharacter(entity as CharacterEntity);
            }
            entity.MoveForward(-0.2f);
        }

        if (data.EnemyTargetType != Data.EnemyTargetType.None) {
            caster.MoveForward(-1f);
        }
    }

    private void DoHeal(BattleControl battleControl, Data.SkillInfo skillInfo, Data.MonsterStat stat) {
        var data = skillInfo.SkillData;
        int targetCounts = skillInfo.SkillData.AllyTargetCount;
        var selectedTarget = battleControl.SelectedTarget;
        int finalHeal = (int)MathUtils.GetPerTenThousand(data.HealRatio) * stat.AttackPower;
        List<MonsterEntity> monsterList;

        switch (skillInfo.SkillData.AllyTargetType) {
        case Data.AllyTargetType.Single:
            HealTarget(selectedTarget, finalHeal);
            break;
        case Data.AllyTargetType.Combo:
            monsterList = GetMonsterByOrder(targetCounts, selectedTarget);
            foreach (var monster in monsterList) {
                HealTarget(monster, finalHeal);
            }
            break;
        case Data.AllyTargetType.Multi:
            monsterList = GetAllMonsters();
            foreach (var monster in monsterList) {
                HealTarget(monster, finalHeal);
            }
            break;
        case Data.AllyTargetType.Random:
            monsterList = GetRandomMonsters(targetCounts);
            foreach (var monster in monsterList) {
                HealTarget(monster, finalHeal);
            }
            break;
        case Data.AllyTargetType.SelfAndSingle:
            MonsterEntity self = FindMonsterByKey(skillInfo.CharacterKey);
            HealTarget(selectedTarget, finalHeal);
            HealTarget(self, finalHeal);
            break;
        }

        void HealTarget(BattleEntity target, int amount) {
            target.IncreaseHP(amount);
        }
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i < _fillAmounts.Length; ++i) {
            _fillAmounts[i] = 0f;
        }
        for (int i = 0; i <  _currentMonsters.Count; ++i) {
            var monster = _currentMonsters[i];
            _fillAmounts[monster.Order] = monster.GetHPPercent(); 
        }
        return _fillAmounts;
    }

    public List<MonsterEntity> GetMonsterByOrder(int targetCounts, BattleEntity selectedEntity = null) {
        int numOfMonsters = _currentMonsters.Count;
        int startIndex = 0;
        List<MonsterEntity> monsterList = new List<MonsterEntity>();

        if (selectedEntity) {
            monsterList.Add(selectedEntity as MonsterEntity);
        }
        
        for (int i = 0; i < numOfMonsters; ++i) {
            if (selectedEntity == _currentMonsters[i]) {
                startIndex = (i + 1) % numOfMonsters;
                break;
            }
        }

        for (int i = startIndex; monsterList.Count == targetCounts; i = (i + 1) % numOfMonsters) {
            if (_currentMonsters[i] == selectedEntity) break;
            monsterList.Add(_currentMonsters[i]);
        }

        return monsterList;
    }

    public List<MonsterEntity> GetAllMonsters() {
        return _currentMonsters.ToList();
    }

    public List<MonsterEntity> GetRandomMonsters(int targetCounts) {
        int numOfAllies = _currentMonsters.Count;
        var enemyList = _currentMonsters.ToList();

        if (targetCounts < numOfAllies) {
            int diff = numOfAllies - targetCounts;
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, enemyList.Count);
                enemyList.RemoveAt(removeIndex);
            }
        }
        return enemyList;
    }

    public IEnumerator RemoveMonster(MonsterEntity monster) {
        monster.CanRemoveEntity = false;
        monster.ChangeAnimationState("Dead");

        while (!monster.IsAnimationEnd) {
            yield return null;
        }

        _currentMonsters.Remove(monster);
        monster.gameObject.SetActive(false);
    }

    public MonsterEntity FindMonsterByKey(int key) {
        MonsterEntity requestedEntity = null;;
        foreach (MonsterEntity entity in _currentMonsters) {
            if (entity.KeyEquals(key)) {
                requestedEntity = entity;
                break;
            }
        }
        return requestedEntity;
    }
}
