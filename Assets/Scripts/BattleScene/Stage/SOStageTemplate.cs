using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SOStageTemplate", menuName = "Template/StageTemplate")]
public class SOStageTemplate : ScriptableObject {
    public string Key = null;
    public int Round_1_Formation = 0;
    public string[] Round_1_Monster = null;
    public int Round_2_Formation = 0;
    public string[] Round_2_Monster = null;
    public int Round_3_Formation = 0;
    public string[] Round_3_Monster = null;
    public int Round_4_Formation = 0;
    public string[] Round_4_Monster = null;
    public int Round_5_Formation = 0;
    public string[] Round_5_Monster = null;
}
