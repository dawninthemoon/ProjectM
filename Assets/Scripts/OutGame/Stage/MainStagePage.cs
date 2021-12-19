using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStagePage : MonoBehaviour
{
    [SerializeField] private SubStagePage subStagePage;

    [SerializeField] private MainStageButton[] mainStageButtons;

    public void Start()
    {
        Init();
    }

    public void OpenSubStagePage(int stage)
    {
        gameObject.SetActive(false);
        subStagePage.SetActive(() => { gameObject.SetActive(true); });
        subStagePage.SetStage(stage);
    }

    public void Init()
    {
        int lastIndex = 0;

        for (; lastIndex < mainStageButtons.Length; ++lastIndex)
        {
            StageData stageData = StageDataParser.Instance.FindStage(lastIndex + 1);
            if (stageData != null)
            {
                mainStageButtons[lastIndex].gameObject.SetActive(true);
                mainStageButtons[lastIndex].Init(OpenSubStagePage, stageData);
            }
            else
                break;
        }

        for (; lastIndex < mainStageButtons.Length; ++lastIndex)
        {
            mainStageButtons[lastIndex].gameObject.SetActive(false);
        }
    }
}