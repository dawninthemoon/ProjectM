using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D;

public abstract class BattleEntity : MonoBehaviour
{
    [SerializeField] private SpriteAtlas _atlas = null;
    protected int _maxHP;
    protected int _curHP;

    public int CurHP
    { get { return _curHP; } }

    public bool CanRemoveEntity { get; set; }
    protected SpriteAtlasAnimator _animator;
    public bool IsAnimationEnd { get; protected set; }
    private static readonly float AnimationSpeed = 0.5f;

    public void Progress()
    {
        _animator.Progress(_atlas);
    }

    public void LateProgress()
    {
        transform.forward = Camera.main.transform.forward;
    }

    public bool IsOverlapped(Vector2 pos, LayerMask mask)
    {
        bool isOverlapped = false;
        RaycastHit raycastHit;
        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(screenRay, out raycastHit, 100f, mask))
        {
            if (raycastHit.collider.gameObject == gameObject)
            {
                isOverlapped = true;
            }
        }
        return isOverlapped;
    }

    public void ChangeAnimationState(string state, bool loop = false, SpriteAtlasAnimator.OnAnimationEnd callback = null)
    {
        _animator.ChangeAnimation(state, loop, AnimationSpeed, callback ?? OnAnimationEnd);
    }

    public virtual void DecreaseHP(int value)
    {
        SkillManager.GetInstance().ShakeCamera();
        _curHP = Mathf.Max(0, _curHP - value);
    }

    public virtual void IncreaseHP(int value)
    {
        _curHP = Mathf.Min(_maxHP, _curHP + value);
    }

    public float GetHPPercent()
    {
        return (float)_curHP / _maxHP;
    }

    public void MoveForward(float direction)
    {
        float amount = 0.4f;
        transform.DOLocalMoveX(direction * amount, 0.4f).From(true).SetEase(Ease.OutCirc);
    }

    private void OnAnimationEnd()
    {
        IsAnimationEnd = true;
    }

    public abstract float GetFinalDefence();

    public abstract bool KeyEquals(int key);
}