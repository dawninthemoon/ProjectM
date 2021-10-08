using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

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
    public static Vector3 GetTouchPosition(Camera camera) {
        Vector3 pos = Vector3.zero;
        #if UNITY_EDITOR
        pos = Input.mousePosition;
        pos = camera.ScreenToWorldPoint(pos);
        #else
        Touch touch = Input.GetTouch(0);
        pos = touch.position;
        #endif
        pos.z -= 10f;
        return pos;
    }
}

namespace RieslingUtils {
    public static class VectorUtility {
        public static Vector3 ChangeZPos(this Vector3 vec, float z) {
            Vector3 newPosition = new Vector3(vec.x, vec.y, z);
            return newPosition;
        }

        public static void ChangeZPos(this PRS prs, float z) {
            prs.pos = prs.pos.ChangeZPos(z);
        }
    }

    public static class StringUtils {
        private static StringBuilder _stringBuilder = new StringBuilder(64);
        public static string MergeStrings(params string[] strList) {
            _stringBuilder.Clear();
            foreach (string str in strList) {
                _stringBuilder.Append(str);
            }
            return _stringBuilder.ToString();
        }
        public static string GetRandomString(params string[] stringList) {
            return stringList[Random.Range(0, stringList.Length)];
        }
    }

    public static class MathUtils {
        public static float GetPerTenThousand(int value) {
            return value / 10000f;
        }

        public static float GetPerTenThousandWithRound(int value) {
            return Mathf.Round(value / 10000f);
        }

        public static float GetPercent(int value) {
            return value / 100f;
        }
    }
}