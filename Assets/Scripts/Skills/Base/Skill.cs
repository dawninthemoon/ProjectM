using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Skill : MonoBehaviour {
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] Transform _leftTransform = null;
    public SpriteRenderer SprRenderer { get { return _renderer;} }
    [SerializeField] TMP_Text _costText = null;
    static readonly string CancelAreaName = "CardCancelArea";
    static readonly float LongTouchTime = 1.5f;
    Collider2D _detectCollider = null;
    SkillInfo _skillInfo;
    public PRS OriginPRS { get; set; }
    public float CardWidth { get; private set; }
    int _originOrder;
    public bool IsTouching { get; private set; }
    bool _isInCancelArea;
    float _touchTimeAgo;

    public void Initialize(SkillInfo info) {
        _skillInfo = info;
        _detectCollider = GetComponent<Collider2D>();
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        CardWidth = (transform.position.x - _leftTransform.position.x) * 2.5f;
        _costText.text = _skillInfo.requireCost.ToString();
    }

    public void Progress(Vector3 curTouchPos) {
        SkillState state = SkillManager.GetInstance().State;
        if (IsTouching && (state == SkillState.CARD_DRAG)) {
            _touchTimeAgo += Time.deltaTime;
            if (_touchTimeAgo > LongTouchTime) {
                
            }

            SkillType type = _skillInfo.type;
            if ((type == SkillType.ENEMY_RANDOM) || (type == SkillType.FRIENDLY_RANDOM)) {
                Vector3 scale = transform.localScale;
                MoveTransform(new PRS(curTouchPos, Quaternion.identity, scale), false);
            }
            DetectCardArea();
        }
    }

    public bool CanSelectTarget() {
        SkillType type = _skillInfo.type;
        return (type == SkillType.ENEMY_TARGET) || (type == SkillType.FRIENDLY_TARGET);
    }

    public SkillInfo GetSkillInfo() {
        return _skillInfo;
    }

    public void UseSkill(BattleControl battleControl) {
        _skillInfo.skillEffect?.ExecuteSkill(_skillInfo, battleControl);
    }

    public void MoveTransform(PRS prs, bool useTweening, float duration = 0f) {
        if (useTweening) {
            transform.DOMove(prs.pos, duration);
            transform.DORotateQuaternion(prs.rot, duration);
            transform.DOScale(prs.scale, duration);
        }
        else {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
    
    public void SetOrder(int order) {
        _originOrder = order;
        _renderer.sortingOrder = order;
    }

    public void SetMostFrontOrder(bool isEnlarge) {
        _renderer.sortingOrder = isEnlarge ? 100 : _originOrder;
    }

    public void DetectCardArea() {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utility.GetTouchPosition(), Vector3.forward);
        int layer = LayerMask.NameToLayer(CancelAreaName);
        _isInCancelArea = System.Array.Exists(hits, x => x.collider.gameObject.layer.Equals(layer));
    }

    public bool IsOverlapped(Vector2 pos) {
        bool isOverlapped = (Physics2D.OverlapPoint(pos) == _detectCollider);
        return isOverlapped;
    }

    public void OnTouchMoved(Vector2 touchPos) {
        if (IsOverlapped(touchPos)) {
            SelectCard();
        }
    }

    public bool OnTouchUp() {
        bool useSkill = false;
        if (IsTouching) {
            IsTouching = false;
            DeSelectCard();

            if (!_isInCancelArea) {
                useSkill = true;
            }
        }
        return useSkill;
    }

    void SelectCard() {
        if (!IsTouching) {
            IsTouching = true;
            SkillManager.GetInstance().EnlargeCard(true, this);
            if (CanSelectTarget()) {
                SkillManager.GetInstance().SetActiveAimSprite(true);
            }
        }
    }

    void DeSelectCard() {
        SkillManager.GetInstance().EnlargeCard(false, this);
        if (CanSelectTarget()) {
            SkillManager.GetInstance().SetActiveAimSprite(false);
        }
    }
}
