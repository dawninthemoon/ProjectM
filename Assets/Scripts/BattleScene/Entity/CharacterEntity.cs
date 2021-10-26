using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;

public class CharacterEntity : BattleEntity {
    [SerializeField] private string[] _skillCardKeys = null;
    public string[] SkillCardKeys { get { return _skillCardKeys; } }
    private Data.Character _characterData;
    public Data.Character CharacterData { get { return _characterData; } }
    private Data.CharacterStat _characterStatData;
    public Data.CharacterStat CharacterStatData { get { return _characterStatData; } }

    public void Initialize(Data.Character characterData, Data.CharacterStat characterStatData) {
        _characterData = characterData;
        _characterStatData = characterStatData;

        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _characterData.SubName + "_", "Idle", true);
        _maxHP = _characterStatData.BaseHP;
        _curHP = _maxHP;
    }

    public override float GetFinalDefence() {
        float defence = 1f + MathUtils.GetPercent(_characterStatData.DefencePower);
        return defence;
    }

    public void ChangeAnimation(string state, bool loop = false, SpriteAtlasAnimator.OnAnimationEnd callback = null) {
        _animator.ChangeAnimation(state, loop, 1f, callback);
    }

    public bool KeyEquals(int key) {
        return _characterData.Key == key;
    }
}
