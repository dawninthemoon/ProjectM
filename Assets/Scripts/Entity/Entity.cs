using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    [SerializeField] private EntityInfo _info = null;
    Collider2D _detectCollider;
    private int _curHP;

    //public void Initialize(EntityInfo info) {
    public void Initialize() {
        //_info = info;
        _curHP = _info.maxHP;
        _detectCollider = GetComponent<Collider2D>();
    }

    public bool IsOverlapped(Vector2 pos, LayerMask mask) {
        var a = Physics2D.OverlapPoint(pos, mask);
        bool isOverlapped = (Physics2D.OverlapPoint(pos, mask) == _detectCollider);
        return isOverlapped;
    }

    public void DecreaseHP(int value) {
        _curHP = Mathf.Max(0, _curHP - value);
    }

    public void IncreaseHP(int value) {
        _curHP = Mathf.Min(_info.maxHP, _curHP + value);
    }

    public float GetHPPercent() {
        return (float)_curHP / _info.maxHP;
    }
}
