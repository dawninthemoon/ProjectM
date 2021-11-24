using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;

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
            yield return new WaitForSeconds(0.3f);
            if (battleControl.PlayerCtrl.IsDefeated()) break;

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
        }
    }

    private void DoAttack(BattleControl battleControl, Data.SkillInfo skillInfo, Data.MonsterStat stat, MonsterEntity caster) {
        var data = skillInfo.SkillData;
        float criticalDamage = MathUtils.GetPerTenThousand(stat.CriticalDamage);
        float baseDamage = stat.AttackPower * (1f + criticalDamage);
        int targetCounts = data.AttackTypeValue;

        var randomCharacters = battleControl.PlayerCtrl.GetRandomCharacters(1);
        if (randomCharacters.Count == 0) return;
        CharacterEntity selectedTarget = randomCharacters[0];

        int finalDamage;

        switch (data.AttackType) {
        case Data.AttackType.SingleAttack:
            finalDamage = Mathf.FloorToInt(baseDamage / selectedTarget.GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
            AttackTarget(selectedTarget, finalDamage);
            break;
        case Data.AttackType.ComboAttack:
            if (data.AttackType == Data.AttackType.ComboAttack) {
                finalDamage = Mathf.FloorToInt(baseDamage / selectedTarget.GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
                AttackTarget(selectedTarget, finalDamage);
            }
            var eList = battleControl.PlayerCtrl.GetCharacterByOrder(targetCounts, selectedTarget);
            foreach (var e in eList) {
                finalDamage = Mathf.FloorToInt(baseDamage / e.GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
                AttackTarget(e, finalDamage);
            }
            break;
        case Data.AttackType.MultiAttack:
            var characters = battleControl.PlayerCtrl.GetAllCharacters();
            foreach (var e in characters) {
                finalDamage = Mathf.FloorToInt(baseDamage / e.GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
                AttackTarget(e, finalDamage);
            }
            break;
        case Data.AttackType.RandomAttack:
            var character = battleControl.PlayerCtrl.GetRandomCharacters(targetCounts, selectedTarget);
            Debug.Log("a");
            if (character.Count > 0) {
                finalDamage = Mathf.FloorToInt(baseDamage / character[0].GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
                AttackTarget(character[0], finalDamage);
            }
            break;
        }

        void AttackTarget(BattleEntity entity, int amount) {
            entity.DecreaseHP(amount);
            if (entity.CurHP <= 0) {
                battleControl.PlayerCtrl.RemoveCharacter(entity as CharacterEntity);
            }
            entity.MoveForward(0.5f);
        }

        if (data.AttackType != Data.AttackType.None) {
            caster.MoveForward(-1f);
        }
    }

    private void DoHeal(BattleControl battleControl, Data.SkillInfo skillInfo, Data.MonsterStat stat) {
        var data = skillInfo.SkillData;
        int targetCounts = skillInfo.SkillData.HealTypeValue;
        var selectedTarget = battleControl.SelectedTarget;
        int finalHeal = (int)MathUtils.GetPerTenThousand(data.HealRatio) * stat.AttackPower;

        switch (skillInfo.SkillData.HealType) {
        case Data.HealType.SingleHeal:
            HealTarget(selectedTarget, finalHeal);
            break;
        case Data.HealType.ComboHeal:
            var mList = GetMonsterByOrder(targetCounts, selectedTarget);
            foreach (var ch in mList) {
                HealTarget(ch, finalHeal);
            }
            break;
        case Data.HealType.MultiHeal:
            var monsters = GetRandomMonsters(targetCounts, selectedTarget);
            foreach (var ch in monsters) {
                HealTarget(ch, finalHeal);
            }
            break;
        case Data.HealType.RandomHeal:
            var allMonster = GetAllMonsters();
            HealTarget(allMonster[0], finalHeal);
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

    public List<MonsterEntity> GetMonsterByOrder(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfMonsters = _currentMonsters.Count;
        int startIndex = 0;
        List<MonsterEntity> characterList = new List<MonsterEntity>();
        
        for (int i = 0; i < numOfMonsters; ++i) {
            if (ignoreEntity == _currentMonsters[i]) {
                startIndex = (i + 1) % numOfMonsters;
                break;
            }
        }

        int count = 0;
        for (int i = startIndex; i < targetCounts; i = (i + 1) % numOfMonsters) {
            if (_currentMonsters[i] == ignoreEntity) break;
            characterList.Add(_currentMonsters[i]);
            ++count;
            if (count == targetCounts) break;
        }

        return characterList;
    }

    public List<MonsterEntity> GetAllMonsters() {
        return _currentMonsters.ToList();
    }

    public List<MonsterEntity> GetRandomMonsters(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _currentMonsters.Count;
        var enemyList = _currentMonsters.ToList();
        
        if (ignoreEntity) {
            enemyList.Remove(ignoreEntity as MonsterEntity);
        }

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
}
