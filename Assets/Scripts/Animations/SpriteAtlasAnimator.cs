using UnityEngine;
using UnityEngine.U2D;

public class SpriteAtlasAnimator
{
    public delegate void OnAnimationEnd();

    private static readonly float _defaultSpeed = 0.06f;
    private float _animationSpeed;
    private float _animationDelay;
    private string _prefix;
    private float _indexTimer = 0f;
    private int _spriteIndex = 1;
    public string AnimationName { get; private set; }
    private bool _loop;

    public int SpriteIndex
    { set { _spriteIndex = value; } get { return _spriteIndex; } }

    private OnAnimationEnd _animationEndCallback;
    private SpriteRenderer _renderer;
    private SOCameraSetting _cameraSettings;

    public SpriteAtlasAnimator(SpriteRenderer renderer, string prefix, string idleStateName, bool loop = false, float speed = 1f)
    {
        _cameraSettings = Resources.Load<SOCameraSetting>("Settings/CameraSettings");
        _renderer = renderer;
        _prefix = prefix;
        ChangeAnimation(idleStateName, loop, speed);
    }

    public void ChangeAnimation(string name, bool loop = false, float speed = 1f, OnAnimationEnd callback = null)
    {
        _indexTimer = _defaultSpeed;
        _spriteIndex = 1;
        _animationSpeed = speed;
        AnimationName = name;
        _loop = loop;
        _animationEndCallback = callback;
    }

    public void SetAnimationDelay(float amount)
    {
        _animationDelay = amount * _cameraSettings.hitStopSeconds;
    }

    public void Progress(SpriteAtlas atlas)
    {
        _indexTimer += Time.deltaTime * _animationSpeed;
        if (_indexTimer > _defaultSpeed)
        {
            _indexTimer -= _defaultSpeed + _animationDelay;
            _animationDelay = 0f;
            var currentFrame = GetSprite();
            if (currentFrame == null)
            {
                if (_loop)
                {
                    _spriteIndex = 1;
                    currentFrame = GetSprite();
                }
                else
                {
                    _animationEndCallback?.Invoke();
                }
            }

            if (currentFrame != null)
            {
                _renderer.sprite = currentFrame;
                ++_spriteIndex;
            }
        }

        Sprite GetSprite()
        {
            string spriteName = RieslingUtils.StringUtils.MergeStrings(_prefix, AnimationName, _spriteIndex.ToString());
            var currentFrame = atlas.GetSprite(spriteName);
            return currentFrame;
        }
    }
}