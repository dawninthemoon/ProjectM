using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUICanvas : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad( this );
    }
}
