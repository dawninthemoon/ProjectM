using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEntity : BattleEntity {
    private Data.Monster _monsterData;
    public Data.Monster MonsterData { get { return _monsterData; } }

    private Data.MonsterStat _monsterStatData;
    public Data.MonsterStat MonsterStatData { get { return _monsterStatData; } }

    public void Initialize() {
        _monsterData = Data.MonsterDataParser.Instance.GetMonster(1);
        _monsterStatData = Data.MonsterStatDataParser.Instance.GetMonsterStat(_monsterData.Key);

        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _monsterData.Name + "_", "IDLE", true, 0.5f);
        _maxHP = _monsterStatData.BaseHP;
        _curHP = _maxHP;
    }
}
