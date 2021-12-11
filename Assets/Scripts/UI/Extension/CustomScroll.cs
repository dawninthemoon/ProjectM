using UnityEngine.EventSystems;
using UnityEngine;
using Utills;
using UnityEngine.UI;
using DG.Tweening;


public class CustomScroll : ScrollRect
{
    public float Velocity => horizontalScrollbar != null ? velocity.x : velocity.y;
    public Scrollbar Scrollbar => horizontalScrollbar ?? verticalScrollbar;

    private float _time;
    private Image _handleImage;
    private Sequence _invisibleSequence;

    protected override void Awake()
    {
        base.Awake();

        if (!Application.isPlaying)
            return;

        _time = 0f;

        _handleImage = Scrollbar.handleRect.GetComponent<Image>();

        if (_handleImage == null)
            return;

        _handleImage.color = _handleImage.color.ChangeAlpha(0f);

        _invisibleSequence = DOTween.Sequence()
                    .SetAutoKill(false)
                    .Append(_handleImage.DOFade(0f, 0.5f)
                        .SetEase(Ease.OutCubic))
                    .Pause();
    }

    public void Update()
    {
        if (!Application.isPlaying)
            return;

        if (_handleImage == null)
            return;

        var isScrolling = Mathf.Abs(Velocity) > 10;

        if(isScrolling)
        {
            _time = 0f;

            if (_handleImage.color.a <= 0)
            {
                _invisibleSequence.Pause();
                _handleImage.color = Color.white;
            }

            return;
        }

        if (_handleImage.color.a <= 0)
            return;

        if (_invisibleSequence.IsPlaying())
            return;

        _time += Time.deltaTime;

        if(_time >= 1f)
        {
            StopMovement();
            _invisibleSequence.Restart();
        }
    }
}
