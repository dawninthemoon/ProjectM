using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : BattleEntity {
    [SerializeField] private int _key = 0;
    private Data.Character _characterData;
    private Data.CharacterStat _characterStatData;

    public override void Initialize() {
        _characterData = Data.CharacterDataParser.Instance.GetCharacter(_key);
        _characterStatData = Data.CharacterStatDataParser.Instance.GetCharacterStat(_characterData.Key);

        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _characterData.SubName + "_", "IDLE", true, 0.5f);
        _maxHP = _characterStatData.BaseHP;
        _curHP = _maxHP;
    }
}
