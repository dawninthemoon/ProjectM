using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Data;
using RieslingUtils;

public class Skill : MonoBehaviour {
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] Transform _leftTransform = null;
    private MeshRenderer _costTextRenderer;
    [SerializeField] private TMP_Text _costText = null;
    [SerializeField] private LayerMask _cardMask;
    private static readonly string CancelAreaName = "CardCancelArea";
    private static readonly float LongTouchTime = 1.5f;
    private Collider2D _detectCollider = null;
    private SkillInfo _skillInfo;
    public PRS OriginPRS { get; set; }
    public float CardWidth { get; private set; }
    private int _originOrder;
    public bool IsTouching { get; private set; }
    private bool _isInCancelArea;
    private float _touchTimeAgo;

    public void Initialize(SkillInfo info) {
        _skillInfo = info;
        _detectCollider = GetComponent<Collider2D>();
        _costTextRenderer = GetComponentInChildren<MeshRenderer>();
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        CardWidth = (transform.position.x - _leftTransform.position.x) * 2.5f;
        _costText.text = _skillInfo.SkillData.Cost.ToString();
    }

    public int GetRequireCost() => _skillInfo.SkillData.Cost;

    public void Progress(Vector3 touchPosition) {
        SkillState state = SkillManager.GetInstance().State;

        if (IsTouching && CanSelectTarget()) {
            SkillManager.GetInstance().SetAimPosition(touchPosition);
        }

        if (IsTouching && (state == SkillState.CARD_DRAG)) {
            _touchTimeAgo += Time.deltaTime;
            if (_touchTimeAgo > LongTouchTime) {
                
            }

            if (_skillInfo.SkillData.CastType == CastType.NoneCast) {
                Vector3 scale = transform.localScale;
                MoveTransform(new PRS(touchPosition, Quaternion.identity, scale), false);
            }
            DetectCardArea(touchPosition);
        }
    }

    public bool CanSelectTarget() {
        return (_skillInfo.SkillData.CastType != CastType.NoneCast);
    }

    public SkillInfo GetSkillInfo() {
        return _skillInfo;
    }

    public void UseSkill(BattleControl battleControl) {
        DoAttack(battleControl);
        DoHeal(battleControl);
    }

    private void DoAttack(BattleControl battleControl) {
        CharacterStat stat = CharacterStatDataParser.Instance.GetCharacterStat(_skillInfo.CharacterKey);

        var data = _skillInfo.SkillData;
        float criticalDamage = MathUtils.GetPerTenThousand(stat.CriticalDamage);
        float baseDamage = stat.AttackPower * (1f + criticalDamage);
        int targetCounts = data.AttackTypeValue;
        var selectedTarget = battleControl.SelectedTarget;
        int finalDamage;

        switch (data.AttackType) {
        case AttackType.SingleAttack:
            finalDamage = Mathf.FloorToInt(baseDamage / selectedTarget.GetFinalDefence() * MathUtils.GetPercent(data.AttackRatio));
            AttackTarget(selectedTarget, finalDamage);
            break;
        case AttackType.SideTurnAttack:
        case AttackType.MultiAttack:
            if (data.AttackType == AttackType.SideTurnAttack) {
                finalDamage = Mathf.FloorToInt(baseDamage / selectedTarget.GetFinalDefence() * MathUtils.GetPercent(data.AttackRatio));
                AttackTarget(selectedTarget, finalDamage);
            }
            var enemies = battleControl.EnemyCtrl.GetRandomEnemies(targetCounts, selectedTarget);
            foreach (var e in enemies) {
                finalDamage = Mathf.FloorToInt(baseDamage / e.GetFinalDefence() * MathUtils.GetPercent(data.AttackRatio));
                AttackTarget(e, finalDamage);
            }
            break;
        case AttackType.RandomAttack:
            var enemy = battleControl.EnemyCtrl.GetRandomEnemies(1);
            finalDamage = Mathf.FloorToInt(baseDamage / enemy[0].GetFinalDefence() * MathUtils.GetPercent(data.AttackRatio));
            AttackTarget(enemy[0], finalDamage);
            break;
        }

        void AttackTarget(BattleEntity entity, int amount) {
            entity.DecreaseHP(amount);
        }
    }

    private void DoHeal(BattleControl battleControl) {
        CharacterStat stat = CharacterStatDataParser.Instance.GetCharacterStat(_skillInfo.CharacterKey);

        var data = _skillInfo.SkillData;
        float baseHeal = MathUtils.GetPerTenThousand(data.HealRatio);
        int targetCounts = _skillInfo.SkillData.HealTypeValue;
        var selectedTarget = battleControl.SelectedTarget;
        int finalHeal = 1;

        switch (_skillInfo.SkillData.HealType) {
        case HealType.SingleHeal:
            finalHeal = Mathf.FloorToInt(baseHeal / selectedTarget.GetFinalDefence() * MathUtils.GetPercent(data.AttackRatio));
            HealTarget(selectedTarget, finalHeal);
            break;
        case HealType.SideTurnHeal:
        case HealType.MultiHeal:
            var characters = battleControl.PlayerCtrl.GetRandomAllies(targetCounts, selectedTarget);
            foreach (var ch in characters) {
                HealTarget(ch, finalHeal);
            }
            break;
        case HealType.RandomHeal:
            var character = battleControl.PlayerCtrl.GetRandomAllies(1);
            HealTarget(character[0], finalHeal);
            break;
        }

        void HealTarget(BattleEntity target, int amount) {
            target.IncreaseHP(amount);
        }
    }

    public void MoveTransform(PRS prs, bool useTweening, float duration = 0f) {
        if (useTweening) {
            transform.DOMoveX(prs.pos.x, duration);
            transform.DOMoveY(prs.pos.y, duration);
            transform.DORotateQuaternion(prs.rot, duration);
            transform.DOScale(prs.scale, duration);
        }
        else {
            prs.pos.z = transform.position.z;
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
    
    public void SetOrder(string sortingLayerName, int order) {
        _originOrder = order;
        _renderer.sortingLayerName = sortingLayerName;
        _renderer.sortingOrder = order;
        _costTextRenderer.sortingOrder = order + 1;
    }

    public void SetMostFrontOrder(bool isEnlarge) {
        _renderer.sortingOrder = isEnlarge ? 100 : _originOrder;
        _costTextRenderer.sortingOrder = isEnlarge ? 101 : _originOrder;
    }

    public void DetectCardArea(Vector2 touchPosition) {
        RaycastHit2D[] hits = Physics2D.RaycastAll(touchPosition, Vector3.forward);
        int layer = LayerMask.NameToLayer(CancelAreaName);
        _isInCancelArea = System.Array.Exists(hits, x => x.collider.gameObject.layer.Equals(layer));
    }

    public bool IsOverlapped(Vector2 pos) {
        bool isOverlapped = false;

        isOverlapped = (Physics2D.OverlapPoint(pos, _cardMask) == _detectCollider);
        return isOverlapped;
    }

    public bool OnTouchMoved(Vector2 touchPos, bool isCostEnough) {
        bool isSelected = false;
        if (IsOverlapped(touchPos)) {
            Debug.Log(_skillInfo.SkillData.Name);
            SkillManager.GetInstance().EnlargeCard(true, this);
            if (!IsTouching && isCostEnough) {
                IsTouching = true;
                if (CanSelectTarget()) {
                    SkillManager.GetInstance().SetActiveAimSprite(true);
                }
                isSelected = true;
            }
        }
        else {
            SkillManager.GetInstance().EnlargeCard(false, this);
        }
        return isSelected;
    }

    public bool OnTouchUp() {
        bool isSkillUsed = false;
        if (IsTouching) {
            IsTouching = false;
            DeSelectCard();

            if (!_isInCancelArea) {
                isSkillUsed = true;
            }
        }
        return isSkillUsed;
    }

    public void DeSelectCard() {
        if (CanSelectTarget()) {
            SkillManager.GetInstance().SetActiveAimSprite(false);
        }
        SkillManager.GetInstance().EnlargeCard(false, this);
    }
}
