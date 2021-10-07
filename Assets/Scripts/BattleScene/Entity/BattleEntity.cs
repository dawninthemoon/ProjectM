using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BattleEntity : MonoBehaviour {
    [SerializeField] SpriteAtlas _atlas = null;
    [SerializeField] private EntityInfo _info = null;
    Collider2D _detectCollider;
    private int _curHP;
    private SpriteAtlasAnimator _animator;

    //public void Initialize(EntityInfo info) {
    public void Initialize() {
        //_info = info;
        _curHP = _info.maxHP;
        _detectCollider = GetComponent<Collider2D>();
        _animator = new SpriteAtlasAnimator(GetComponent<SpriteRenderer>(), _info.name + "_", "IDLE", true);
    }

    public void Progress() {
        _animator.Progress(_atlas);
    }

    public bool IsOverlapped(Vector2 pos, LayerMask mask) {
        bool isOverlapped = false;// = (Physics2D.OverlapPoint(pos, mask) == _detectCollider);
        RaycastHit raycastHit;
        Ray screenRay = Camera.main.ScreenPointToRay( Input.mousePosition);
        Debug.DrawRay(screenRay.origin,screenRay.direction, Color.cyan, 4);

        if( Physics.Raycast( screenRay, out raycastHit, 100, mask) )
        {
            isOverlapped = true;
            Debug.Log( raycastHit.transform.gameObject.name );
        }
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
