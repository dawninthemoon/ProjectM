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
    CardInfo _cardInfo;
    public PRS OriginPRS { get; set; }
    public float CardWidth { get; private set; }
    int _originOrder;

    public void Initialize(CardInfo info) {
        _cardInfo = info;
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        CardWidth = (transform.position.x - _leftTransform.position.x) * 2.5f;
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

    void OnMouseDown() {
        CardManager.GetInstance().EnlargeCard(true, this);

    }

    void OnMouseOver() {
    }

    void OnMouseExit() {
    }

    void OnMouseUp() {
        CardManager.GetInstance().EnlargeCard(false, this);
    }
}
