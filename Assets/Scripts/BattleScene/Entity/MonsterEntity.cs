using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;
using DG.Tweening;

public class MonsterEntity : BattleEntity {
    private class SkillStatus {
        public Data.SkillData skillData;
        public int remainTurnCount;
        public SkillStatus(Data.SkillData skillData, int remainTurnCount) {
            this.skillData = skillData;
            this.remainTurnCount = remainTurnCount;
        }
    }

    [SerializeField] private int[] _skillKeys = null;

    private SkillStatus[] _skillStatus;

    private Data.Monster _monsterData;
    public Data.Monster MonsterData { get { return _monsterData; } }

    private Data.MonsterStat _monsterStatData;
    public Data.MonsterStat MonsterStatData { get { return _monsterStatData; } }
    public int Order { get; private set; }
    public int Key { get; private set; }
    private int _skillIndex = 0;
    public bool IsAnimationEnd { get; private set; }

    public void Initialize(int key, int order) {
        Key = key;
        _monsterData = Data.MonsterDataParser.Instance.GetMonster(key);
        _monsterStatData = Data.MonsterStatDataParser.Instance.GetMonsterStat(_monsterData.Level);

        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _monsterData.Name + "_", "Idle", true, 0.5f);
        _maxHP = _monsterStatData.BaseHP / 10;
        _curHP = _maxHP;

        int numOfSkills = _skillKeys.Length;
        _skillStatus = new SkillStatus[numOfSkills];
        for (int i = 0; i < numOfSkills; ++i) {
            var skillData = Data.SkillDataParser.Instance.GetSkillData(_skillKeys[i]);
            _skillStatus[i] = new SkillStatus(skillData, 0);
        }

        Order = order;
    }

    public void ChangeAnimationState(string state, bool loop = false, SpriteAtlasAnimator.OnAnimationEnd callback = null) {
        IsAnimationEnd = false;
        _animator.ChangeAnimation(state, loop, 0.5f, callback ?? OnAnimationEnd);
    }

    public override float GetFinalDefence() {
        float defence = 1f + MathUtils.GetPercent(_monsterStatData.DefencePower);
        return defence;
    }
    public void SetAnimationDelay(float amount) {
        _animator.SetAnimationDelay(amount);
    }

    private void OnAnimationEnd() {
        IsAnimationEnd = true;
    }

    public Data.SkillData GetCurrentSkillData() {
        int startIndex = _skillIndex;

        while (true) {
            if (_skillStatus[_skillIndex].remainTurnCount > 0) {
                ++_skillIndex;
            }
            else {
                break;
            }

            if (_skillIndex == startIndex) return null;
        }

        return _skillStatus[_skillIndex++].skillData;
    }

    public override void DecreaseHP(int value) {
        base.DecreaseHP(value);
        ChangeAnimationState("Hit", false, ChangeToIdle);
    }

    private void ChangeToIdle() {
        if (_curHP <= 0) {
            CanRemoveEntity = true;
        }
        ChangeAnimationState("Idle", true);
    }
}
