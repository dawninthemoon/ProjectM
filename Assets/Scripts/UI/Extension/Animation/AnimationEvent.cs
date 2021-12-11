using UnityEngine;
using System;

public enum AnimationMode
{
    None,
    Start,
    Loop,
    End,
    CustomEvent1,
    CustomEvent2,
    CustomEvent3,
}

[RequireComponent(typeof(Animator))]
public class AnimationEvent : MonoBehaviour
{
    public Action<AnimationMode> ModeCallback { get; set; }
    public Action<int> IndexCallback { get; set; }
    public Action<string> StringCallback { get; set; }

    public void SetMode(AnimationMode mode)
    {
        ModeCallback?.Invoke(mode);
    }

    public void SetIndex(int index)
    {
        IndexCallback?.Invoke(index);
    }

    public void SetString(string str)
    {
        StringCallback?.Invoke(str);
    }
}
