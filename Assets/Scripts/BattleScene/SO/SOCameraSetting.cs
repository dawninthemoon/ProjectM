using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SOCameraSetting", menuName = "Template/CameraSetting")]
public class SOCameraSetting : ScriptableObject {
    public float duration;
    public float strength;
    public int vibrato;
    public float randomness;
    public bool snapping;
    public bool fadeOut;
    public float hitStopSeconds;
}
