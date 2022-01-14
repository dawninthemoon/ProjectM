using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISkillCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image characterIcon;
    private Data.SkillInfo skillInfo;

    private int index;
    public int Index
    {
        get { return index; }
    }
    public event System.Action<int> OnSelectEvent;
    public event System.Action<int> OnDeselectEvent;

    public void Init( int index, System.Action<int> onSelectCallback, System.Action<int> onDeselectCallback )
    {
        this.index = index;
        OnSelectEvent += onSelectCallback;
        OnDeselectEvent += onDeselectCallback;
    }


    public void OnPointerDown( PointerEventData pointerEventData )
    {
        OnSelectEvent?.Invoke(index);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        OnDeselectEvent?.Invoke(index);
    }
}
