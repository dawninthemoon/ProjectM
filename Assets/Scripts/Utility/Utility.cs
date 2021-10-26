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

public static class Utility {
    public static Vector3 GetTouchPosition() {
        Vector3 pos = Vector3.zero;
        #if UNITY_EDITOR
        pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        #else
        Touch touch = Input.GetTouch(0);
        pos = touch.position;
        #endif
        pos.z -= 10f;
        return pos;
    }
}