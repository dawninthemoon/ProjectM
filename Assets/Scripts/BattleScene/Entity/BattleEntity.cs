using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public abstract class BattleEntity : MonoBehaviour {
    [SerializeField] private SpriteAtlas _atlas = null;
    protected int _maxHP;
    protected int _curHP;
    protected SpriteAtlasAnimator _animator;

    public void Progress() {
        _animator.Progress(_atlas);
    }

    public bool IsOverlapped(Vector2 pos, LayerMask mask) {
        bool isOverlapped = false;
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
        _curHP = Mathf.Min(_maxHP, _curHP + value);
    }

    public float GetHPPercent() {
        return (float)_curHP / _maxHP;
    }
}
