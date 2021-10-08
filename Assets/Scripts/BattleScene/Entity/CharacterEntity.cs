using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : BattleEntity {
    [SerializeField] private int _key = 0;
    private Data.Character _characterData;
    private Data.CharacterStat _characterStatData;

    public void Initialize(Data.Character characterData, Data.CharacterStat characterStatData) {
        _characterData = characterData;
        _characterStatData = characterStatData;

        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _characterData.SubName + "_", "IDLE", true, 0.5f);
        _maxHP = _characterStatData.BaseHP;
        _curHP = _maxHP;
    }
}
