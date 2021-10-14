using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;

public class MonsterControl : MonoBehaviour {
    [SerializeField] private int[] _monsterKeys = null;
    [SerializeField] private Vector3[] _initialPosition = null;
    [SerializeField] LayerMask _layerMask;
    private List<MonsterEntity> _currentMonsters;
    private float[] _fillAmounts;
    private static readonly string MonsterPreafabPath = "MonsterEntityPrefab/";

    public void Initialize() {
        _fillAmounts = new float[4];

        int monsterCounts = _monsterKeys.Length;
        _currentMonsters = new List<MonsterEntity>(monsterCounts);

        for (int i = 0; i < monsterCounts; ++i) {
            var monsterData = Data.MonsterDataParser.Instance.GetMonster(_monsterKeys[i]);
            var prefab = ResourceManager.GetInstance().GetEntityPrefab(MonsterPreafabPath + monsterData.MonsterPrefab);

            BattleEntity monster = Instantiate(prefab, _initialPosition[i], Quaternion.identity);
            _currentMonsters.Add(monster as MonsterEntity);
            _currentMonsters[i].Initialize(_monsterKeys[i], i);
        }
    }

    public void Progress() {
        for (int i = 0; i < _currentMonsters.Count; ++i) {
            _currentMonsters[i].Progress();
        }
    }

    public MonsterEntity GetSelectedEnemy(Vector3 touchPos) {
        MonsterEntity target = null;
        int monsterCounts = _currentMonsters.Count;
        for (int i = 0; i < monsterCounts; ++i) {
            if (_currentMonsters[i].IsOverlapped(touchPos, _layerMask)) {
                target = _currentMonsters[i];
            }
        }
        return target;
    }

    public void UseSkill(BattleControl battleControl) {
        foreach (MonsterEntity monster in _currentMonsters) {
            Data.SkillData skillData = monster.GetCurrentSkillData();
            if (skillData == null) continue;
            Data.SkillInfo skillInfo = new Data.SkillInfo(skillData, monster.Key);

            int grade = (int)Data.MonsterDataParser.Instance.GetMonster(skillInfo.CharacterKey).Level;
            Data.MonsterStat stat = Data.MonsterStatDataParser.Instance.GetMonsterStat(grade);

            string name = Data.MonsterDataParser.Instance.GetMonster(skillInfo.CharacterKey).Name;
            Debug.Log(name + "이 " + skillInfo.SkillData.Name + "을 사용!");

            DoAttack(battleControl, skillInfo, stat);
            DoHeal(battleControl, skillInfo, stat);
        }
    }

    private void DoAttack(BattleControl battleControl, Data.SkillInfo skillInfo, Data.MonsterStat stat) {
        var data = skillInfo.SkillData;
        float criticalDamage = MathUtils.GetPerTenThousand(stat.CriticalDamage);
        float baseDamage = stat.AttackPower * (1f + criticalDamage);
        int targetCounts = data.AttackTypeValue;
        var selectedTarget = battleControl.PlayerCtrl.GetRandomCharacters(1)[0];
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
            finalDamage = Mathf.FloorToInt(baseDamage / character[0].GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
            AttackTarget(character[0], finalDamage);
            break;
        }

        void AttackTarget(BattleEntity entity, int amount) {
            entity.DecreaseHP(amount);
            if (entity.CurHP <= 0) {
                battleControl.PlayerCtrl.RemoveCharacter(entity as CharacterEntity);
            }
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
        int numOfAllies = _currentMonsters.Count;
        int startIndex = 0;
        List<MonsterEntity> characterList = new List<MonsterEntity>();
        
        for (int i = 0; i < numOfAllies; ++i) {
            if (ignoreEntity == _currentMonsters[i]) {
                startIndex = (i + 1) % numOfAllies;
                break;
            }
        }

        for (int i = startIndex; i < targetCounts; i = (i + 1) % numOfAllies) {
            if (_currentMonsters[i] == ignoreEntity) break;
            characterList.Add(_currentMonsters[i]);
            
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

    public void RemoveMonster(MonsterEntity monster) {
        _currentMonsters.Remove(monster);
        monster.gameObject.SetActive(false);
    }
}
