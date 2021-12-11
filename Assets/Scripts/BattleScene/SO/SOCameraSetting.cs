using UnityEngine;

#if UNITY_EDITOR
#endif

[CreateAssetMenu(fileName = "SOCameraSetting", menuName = "Template/CameraSetting")]
public class SOCameraSetting : ScriptableObject
{
    public float duration;
    public float strength;
    public int vibrato;
    public float randomness;
    public bool snapping;
    public bool fadeOut;
    public float hitStopSeconds;
}