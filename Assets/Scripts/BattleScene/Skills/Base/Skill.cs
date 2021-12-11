using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer = null;
    [SerializeField] private SpriteRenderer _iconRenderer = null;
    [SerializeField] private SpriteRenderer _characterIconRenderer = null;
    [SerializeField] private Transform _leftTransform = null;
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

    public void Initialize(SkillInfo info)
    {
        _skillInfo = info;
        _detectCollider = GetComponent<Collider2D>();
        _costTextRenderer = GetComponentInChildren<MeshRenderer>();
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        CardWidth = (transform.position.x - _leftTransform.position.x) * 2.5f;
        _costText.text = _skillInfo.SkillData.Cost.ToString();
        _iconRenderer.sprite = ResourceManager.GetInstance().GetSprite("SkillIcon/" + _skillInfo.SkillData.IconKey);

        string characterSubName = Data.CharacterDataParser.Instance.GetCharacter(info.CharacterKey).SubName;
        _characterIconRenderer.sprite = ResourceManager.GetInstance().GetSprite(characterSubName);
    }

    public SkillInfo GetSkillInfo() => _skillInfo;

    public int GetRequireCost() => _skillInfo.SkillData.Cost;

    public void Progress(Vector3 touchPosition)
    {
        SkillState state = SkillManager.Instance.State;

        if (IsTouching && CanSelectTarget())
        {
            SkillManager.Instance.SetAimPosition(touchPosition);
        }

        if (IsTouching && (state == SkillState.CARD_DRAG))
        {
            _touchTimeAgo += Time.deltaTime;
            if (_touchTimeAgo > LongTouchTime)
            {
            }

            if (_skillInfo.SkillData.CastType == CastType.NoneCast)
            {
                Vector3 scale = transform.localScale;
                MoveTransform(new PRS(touchPosition, Quaternion.identity, scale), false);
            }
            DetectCardArea(touchPosition);
        }
    }

    public bool CanSelectTarget()
    {
        return (_skillInfo.SkillData.CastType != CastType.NoneCast);
    }

    public bool IsAllyTarget()
    {
        var data = _skillInfo.SkillData;
        bool isCharacterTarget = data.CastType == Data.CastType.TeamCast;
        return isCharacterTarget;
    }

    public void MoveTransform(PRS prs, bool useTweening, float duration = 0f)
    {
        if (useTweening)
        {
            transform.DOMoveX(prs.pos.x, duration);
            transform.DOMoveY(prs.pos.y, duration);
            transform.DORotateQuaternion(prs.rot, duration);
            transform.DOScale(prs.scale, duration);
        }
        else
        {
            prs.pos.z = transform.position.z;
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    public void SetOrder(string sortingLayerName, int order)
    {
        _originOrder = order;

        _renderer.sortingLayerName = sortingLayerName;
        _iconRenderer.sortingLayerName = _characterIconRenderer.sortingLayerName = sortingLayerName;

        _renderer.sortingOrder = order;
        _iconRenderer.sortingOrder = _characterIconRenderer.sortingOrder = order + 1;

        _costTextRenderer.sortingOrder = order + 2;
    }

    public void SetMostFrontOrder(bool isEnlarge)
    {
        _renderer.sortingOrder = isEnlarge ? 100 : _originOrder;
        _iconRenderer.sortingOrder = _characterIconRenderer.sortingOrder = isEnlarge ? 101 : _originOrder + 1;
        _costTextRenderer.sortingOrder = isEnlarge ? 102 : _originOrder + 2;
    }

    public void DetectCardArea(Vector2 touchPosition)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(touchPosition, Vector3.forward);
        int layer = LayerMask.NameToLayer(CancelAreaName);
        _isInCancelArea = System.Array.Exists(hits, x => x.collider.gameObject.layer.Equals(layer));
    }

    public bool IsOverlapped(Vector2 pos)
    {
        bool isOverlapped = false;

        isOverlapped = (Physics2D.OverlapPoint(pos, _cardMask) == _detectCollider);
        return isOverlapped;
    }

    public bool OnTouchMoved(Vector2 touchPos, bool isCostEnough)
    {
        bool isSelected = false;
        if (IsOverlapped(touchPos))
        {
            if (!IsTouching && isCostEnough)
            {
                SkillManager.Instance.EnlargeCard(true, this);
                IsTouching = true;
                if (CanSelectTarget())
                {
                    SkillManager.Instance.SetActiveAimSprite(true);
                }
                isSelected = true;
            }
        }
        else
        {
            SkillManager.Instance.EnlargeCard(false, this);
        }
        return isSelected;
    }

    public bool OnTouchUp()
    {
        bool isSkillUsed = false;
        if (IsTouching)
        {
            IsTouching = false;
            DeSelectCard();

            if (!_isInCancelArea)
            {
                isSkillUsed = true;
            }
        }
        return isSkillUsed;
    }

    public void DeSelectCard()
    {
        if (CanSelectTarget())
        {
            SkillManager.Instance.SetActiveAimSprite(false);
        }
        SkillManager.Instance.EnlargeCard(false, this);
    }
}