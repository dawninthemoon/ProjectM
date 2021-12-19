using Data;
using UnityEngine;

public class SubStagePage : MonoBehaviour
{
    [SerializeField] private SubStageButton[] subStageButtons;

    public void SetActive(System.Action backButtonCallback)
    {
        gameObject.SetActive(true);
    }

    public void SetDiable()
    {
        gameObject.SetActive(false);
    }

    public void SetStage(int stage)
    {
        StageData[] stageDatas = StageDataParser.Instance.FindAllStage(stage);

        for (int i = 0; i < subStageButtons.Length; ++i)
        {
            if (stageDatas.Length > i)
            {
                subStageButtons[i].gameObject.SetActive(true);
                subStageButtons[i].SetStageData(stageDatas[i]);
            }
            else
            {
                subStageButtons[i].gameObject.SetActive(false);
            }
        }
    }
}