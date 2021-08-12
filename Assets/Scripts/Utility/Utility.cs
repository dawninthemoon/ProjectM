using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PRS {
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;

    public PRS(Vector3 p, Quaternion r, Vector3 s) {
        pos = p;
        rot = r;
        scale = s;
    }
}