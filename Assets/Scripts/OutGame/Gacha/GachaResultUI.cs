using Data;
using UnityEngine;

public class GachaResultUI : MonoBehaviour
{
    [SerializeField] private GachaItemIcon[] gachaItemIcons;

    public void SetUI(RandomBoxData[] randomBoxDatas)
    {
        for (int i = 0; i < gachaItemIcons.Length; ++i)
        {
            if (i >= randomBoxDatas.Length)
            {
                gachaItemIcons[i].gameObject.SetActive(false);
                continue;
            }

            gachaItemIcons[i].gameObject.SetActive(true);
            gachaItemIcons[i].SetIcon(randomBoxDatas[i]);
        }
    }
}