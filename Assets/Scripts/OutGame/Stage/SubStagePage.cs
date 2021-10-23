using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class SubStagePage : MonoBehaviour
{
    [SerializeField] private SubStageButton[] subStageButtons;

    public void SetActive( System.Action backButtonCallback )
    {
        gameObject.SetActive(true);
        TopUIBackButton.Instance.AddCallback( () => { SetDiable(); backButtonCallback?.Invoke(); } );
    }

    public void SetDiable()
    {
        gameObject.SetActive(false);
        TopUIBackButton.Instance.PopCallback();
    }

    public void SetStage( int stage )
    {
        StageData[] stageDatas = StageDataParser.Instance.FindAllStage(stage);

        for( int i = 0; i < subStageButtons.Length; ++i )
        {
            if( stageDatas.Length > i)
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
