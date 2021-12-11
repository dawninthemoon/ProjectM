using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class PressedState
{
    public UIBehaviour target;

    public Color normalColor = Color.white;
    public Color pressedColor = Color.white;
    public Sprite normalSprite;
    public Sprite pressedSprite;
}

public class CustomButton : Button, IDragHandler
{
    [SerializeField] private RectTransform contents;
    [SerializeField] private List<PressedState> pressedStates;
    [SerializeField] private float offset = -8;

    public event Action OnDragAction;

    private Vector2 _originContentsPosition;

    public Action onPointerDown;
    public Action onPointerUp;

    public bool IsInteractable
    {
        get { return interactable; }
        set
        {
            interactable = value;

            if (interactable)
                OnPointUp();
            else
                OnPointDown();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (contents)
        {
            _originContentsPosition = contents.anchoredPosition;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable)
            return;

        base.OnPointerDown(eventData);
        OnPointDown();
        onPointerDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        OnPointUp();
        onPointerUp?.Invoke();
    }

    private void OnPointDown()
    {
        if (contents)
        {
            contents.anchoredPosition = _originContentsPosition + new Vector2(0, offset);
        }

        foreach (var state in pressedStates)
        {
            if (state.target is Graphic graphic)
            {
                if (state.pressedSprite != null && (graphic is Image image))
                {
                    image.sprite = state.pressedSprite;
                }

                graphic.color = state.pressedColor;
            }

            if (state.target is Shadow shadow)
            {
                shadow.effectColor = state.pressedColor;
            }
        }
    }

    private void OnPointUp()
    {
        if (contents)
        {
            contents.anchoredPosition = _originContentsPosition;
        }

        foreach (var state in pressedStates)
        {
            if (state.target is Graphic graphic)
            {
                if (state.normalSprite != null && (graphic is Image image))
                {
                    image.sprite = state.normalSprite;
                }

                graphic.color = state.normalColor;
            }

            if (state.target is Shadow shadow)
            {
                shadow.effectColor = state.normalColor;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragAction?.Invoke();
    }

#if UNITY_EDITOR



#endif
}