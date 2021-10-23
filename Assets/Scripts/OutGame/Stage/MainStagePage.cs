using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Data;

public class MainStagePage : MonoBehaviour
{
    [SerializeField] private SubStagePage subStagePage;

    [SerializeField] private MainStageButton[] mainStageButtons;

    public void Start()
    {
        TopUIBackButton.Instance.AddCallback(() => { SceneManager.LoadScene("Lobby"); });
        Init();
    }

    public void OnDestroy()
    {
        TopUIBackButton.Instance.PopCallback();
    }

    public void OpenSubStagePage( int stage )
    {
        gameObject.SetActive(false);
        subStagePage.SetActive( ()=> { gameObject.SetActive(true); } );
        subStagePage.SetStage(stage);
    }

    public void Init()
    {
        int lastIndex = 0;

        for ( ; lastIndex < mainStageButtons.Length; ++lastIndex)
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

        for(; lastIndex < mainStageButtons.Length; ++lastIndex )
        {
            mainStageButtons[lastIndex].gameObject.SetActive(false);
        }
    }
}
