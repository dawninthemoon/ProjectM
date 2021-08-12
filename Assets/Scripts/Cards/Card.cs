using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour {
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] Transform _leftTransform = null;
    public SpriteRenderer SprRenderer { get { return _renderer;} }
    [SerializeField] TMP_Text _costText = null;
    Collider2D _detectCollider = null;
    CardInfo _cardInfo;
    public PRS OriginPRS { get; set; }
    public float CardWidth { get; private set; }
    int _originOrder;
    bool _isTouching;

    public void Initialize(CardInfo info) {
        _cardInfo = info;
        _detectCollider = GetComponent<Collider2D>();
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        CardWidth = (transform.position.x - _leftTransform.position.x) * 2.5f;
        _costText.text = _cardInfo._requireCost.ToString();
    }

    public void Update() {
        ListenInput();
    }

    void ListenInput() {
        #if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.OverlapPoint(touchPos) == _detectCollider) {
                _isTouching = true;
                SelectCard();
            }
            else if (_isTouching) {
                DeSelectCard();
            }
        }
        else if (_isTouching && Input.GetMouseButtonUp(0)) {
            _isTouching = false;
            DeSelectCard();
        }
        #else
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase) {
            case TouchPhase.Moved:
                if (Physics2D.OverlapPoint(touchPos) == _detectCollider) {
                    _isTouching = true;
                    SelectCard();
                }
                break;
            case TouchPhase.Ended:
                if (_isTouching) {
                    _isTouching = false;
                    DeSelectCard();
                }
                break;
            }
        }
        #endif
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

    void SelectCard() {
        CardManager.GetInstance().EnlargeCard(true, this);
    }

    void DeSelectCard() {
        CardManager.GetInstance().EnlargeCard(false, this);
    }
}
