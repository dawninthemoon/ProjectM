using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;

public class MonsterEntity : BattleEntity {
    private Data.Monster _monsterData;
    public Data.Monster MonsterData { get { return _monsterData; } }

    private Data.MonsterStat _monsterStatData;
    public Data.MonsterStat MonsterStatData { get { return _monsterStatData; } }
    public int Order { get; private set; }

    public void Initialize(int key, int order) {
        _monsterData = Data.MonsterDataParser.Instance.GetMonster(key);
        _monsterStatData = Data.MonsterStatDataParser.Instance.GetMonsterStat(_monsterData.Level);

        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _monsterData.Name + "_", "IDLE", true, 0.5f);
        _maxHP = _monsterStatData.BaseHP / 10;
        _curHP = _maxHP;

        Order = order;
    }

    public override float GetFinalDefence() {
        float defence = 1f + MathUtils.GetPercent(_monsterStatData.DefencePower);
        return defence;
    }
}
