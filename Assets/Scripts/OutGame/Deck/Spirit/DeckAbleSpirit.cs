using PolyAndCode.UI;
using UnityEngine;

public class DeckAbleSpirit : MonoBehaviour, ICell
{
    [SerializeField] private SpiritIcon spiritIcon;
    private int spiritIndex = 0;

    public event System.Action<int> OnClcikEvent;

    public void SetIndex(int index, System.Action<int> OnClickAction)
    {
        spiritIndex = index;
        spiritIcon.SetSpirit(index);
        OnClcikEvent += OnClickAction;
    }

    public void OnClick()
    {
        OnClcikEvent?.Invoke(spiritIndex);
    }
}