using UnityEngine;

public class CharacterSpritSlot : MonoBehaviour
{
    [SerializeField] private SpiritIcon spiritIcon;
    [SerializeField] private GameObject button;

    private int index = 0;

    public void Init(int index)
    {
        this.index = index;
    }

    public void SetSpritData(Data.SpiritData spiritData)
    {
        spiritIcon.SetSpirit(spiritData);
    }

    public void SetNull()
    {
        spiritIcon.SetNull();
    }

    public void ActiveButton()
    {
        button.gameObject.SetActive(true);
    }

    public void DisableButton()
    {
        button.gameObject.SetActive(false);
    }
}