using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : BattleEntity {
    private Data.Character _characterData;

    public override void Initialize() {
        _characterData = Data.CharacterDataParser.Instance.GetCharacter(1);
        //_monsterStatData = Data.MonsterStatDataParser.Instance.GetMonsterStat(_monsterData.Key);

        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _characterData.Name + "_", "IDLE", true);
        //_maxHP = _monsterStatData.BaseHP;
        //_curHP = _maxHP;
    }
}
