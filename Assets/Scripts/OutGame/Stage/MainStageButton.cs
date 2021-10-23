using UnityEngine;
using TMPro;

public class MainStageButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;

    private System.Action<int> onClickCallback;

    private int stageIndex = 0;

    public void Init( System.Action<int> onClickCallback, Data.StageData stageData )
    {
        this.stageIndex = stageData.Stage;
        this.onClickCallback = onClickCallback;
        titleText.text = string.Format("{0}\n-제 {1} 스테이지", stageData.Name, stageData.Stage );
    }

    public void OnClick()
    {
        onClickCallback?.Invoke(stageIndex);
    }
}
