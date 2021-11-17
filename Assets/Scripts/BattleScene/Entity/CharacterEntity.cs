using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;

public class CharacterEntity : BattleEntity {
    private Data.Character _characterData;
    public Data.Character CharacterData { get { return _characterData; } }
    private Data.CharacterStat _characterStatData;
    public Data.CharacterStat CharacterStatData { get { return _characterStatData; } }
    private static readonly float AnimationSpeed = 0.5f;
    public bool IsAnimationEnd { get; private set; }

    public void Initialize(Data.Character characterData, Data.CharacterStat characterStatData) {
        _characterData = characterData;
        _characterStatData = characterStatData;

        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _characterData.SubName + "_", "Idle", true, AnimationSpeed);
        _maxHP = _characterStatData.BaseHP;
        _curHP = _maxHP;
    }

    public override float GetFinalDefence() {
        float defence = 1f + MathUtils.GetPercent(_characterStatData.DefencePower);
        return defence;
    }

    public void ChangeAnimationState(string state, bool loop = false, SpriteAtlasAnimator.OnAnimationEnd callback = null) {
        _animator.ChangeAnimation(state, loop, AnimationSpeed, callback ?? OnAnimationEnd);
    }

    private void OnAnimationEnd() {
        IsAnimationEnd = true;
    }

    public bool KeyEquals(int key) {
        return _characterData.Key == key;
    }

    public override void DecreaseHP(int value) {
        base.DecreaseHP(value);
        ChangeAnimationState("Hit", false, ChangeToIdle);
    }

    public void ChangeToIdle() {
        if (_curHP <= 0) {
            CanRemoveEntity = true;
        }
        ChangeAnimationState("Idle", true);
    }
}
