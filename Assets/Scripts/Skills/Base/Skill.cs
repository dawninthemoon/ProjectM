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
    SkillInfo _cardInfo;
    public PRS OriginPRS { get; set; }
    public float CardWidth { get; private set; }
    int _originOrder;
    public bool IsTouching { get; private set; }
    bool _isInCancelArea;
    float _touchTimeAgo;

    public void Initialize(SkillInfo info) {
        _cardInfo = info;
        _detectCollider = GetComponent<Collider2D>();
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        CardWidth = (transform.position.x - _leftTransform.position.x) * 2.5f;
        _costText.text = _cardInfo.requireCost.ToString();
    }

    public bool Progress(Vector3 curTouchPos) {
        bool selectTarget = false;
        SkillState state = SkillManager.GetInstance().State;
        if (IsTouching && (state == SkillState.CARD_DRAG)) {
            _touchTimeAgo += Time.deltaTime;
            if (_touchTimeAgo > LongTouchTime) {
                
            }

            SkillType type = _cardInfo.type;
            if ((type == SkillType.ENEMY_TARGET) || (type == SkillType.FRIENDLY_TARGET)) {
                selectTarget = true;
            }
            else {
                Vector3 scale = transform.localScale;
                MoveTransform(new PRS(curTouchPos, Quaternion.identity, scale), false);
            }
            DetectCardArea();
        }
        return selectTarget;
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

    public void OnTouchMoved(Vector2 touchPos) {
        if (Physics2D.OverlapPoint(touchPos) == _detectCollider) {
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
        }
    }

    void DeSelectCard() {
        SkillManager.GetInstance().EnlargeCard(false, this);
    }
}
