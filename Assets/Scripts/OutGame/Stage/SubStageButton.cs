using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SubStageButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainStageText;
    [SerializeField] private TextMeshProUGUI subStageText;

    private Data.StageData currentStageData;

    public void SetStageData( Data.StageData stageData )
    {
        mainStageText.text = stageData.Name;
        subStageText.text = stageData.SubName;

        currentStageData = stageData;
    }

    public void OnClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
