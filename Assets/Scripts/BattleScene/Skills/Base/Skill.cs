using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using Data;

public class Skill : MonoBehaviour {
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] Transform _leftTransform = null;
    private MeshRenderer _costTextRenderer;
    [SerializeField] TMP_Text _costText = null;
    [SerializeField] LayerMask _cardMask;
    static readonly string CancelAreaName = "CardCancelArea";
    static readonly float LongTouchTime = 1.5f;
    Collider2D _detectCollider = null;
    SkillData _skillData;
    public PRS OriginPRS { get; set; }
    public float CardWidth { get; private set; }
    int _originOrder;
    public bool IsTouching { get; private set; }
    bool _isInCancelArea;
    float _touchTimeAgo;

    public void Initialize(SkillData data) {
        _skillData = data;
        _detectCollider = GetComponent<Collider2D>();
        _costTextRenderer = GetComponentInChildren<MeshRenderer>();
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        CardWidth = (transform.position.x - _leftTransform.position.x) * 2.5f;
        _costText.text = _skillData.Cost.ToString();
    }

    public int GetRequireCost() => _skillData.Cost;

    public void Progress(Vector3 touchPosition) {
        SkillState state = SkillManager.GetInstance().State;

        if (IsTouching && CanSelectTarget()) {
            SkillManager.GetInstance().SetAimPosition(touchPosition);
        }

        if (IsTouching && (state == SkillState.CARD_DRAG)) {
            _touchTimeAgo += Time.deltaTime;
            if (_touchTimeAgo > LongTouchTime) {
                
            }

            if (_skillData.CastType == CastType.NoneCast) {
                Vector3 scale = transform.localScale;
                MoveTransform(new PRS(touchPosition, Quaternion.identity, scale), false);
            }
            DetectCardArea(touchPosition);
        }
    }

    public bool CanSelectTarget() {
        return (_skillData.CastType != CastType.NoneCast);
    }

    public SkillData GetSkillInfo() {
        return _skillData;
    }

    public void UseSkill(BattleControl battleControl) {
        //_skillInfo.skillEffect?.ExecuteSkill(_skillInfo, battleControl);
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
        bool isOverlapped = (Physics2D.OverlapPoint(pos, _cardMask) == _detectCollider);
        return isOverlapped;
    }

    public bool OnTouchMoved(Vector2 touchPos, bool isCostEnough) {
        bool isSelected = false;
        if (IsOverlapped(touchPos)) {
            Debug.Log(_skillData.Name);
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
