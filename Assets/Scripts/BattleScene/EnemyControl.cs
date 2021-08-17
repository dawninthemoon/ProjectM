using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    [SerializeField] Entity[] _enemies = null;
    [SerializeField] LayerMask _layerMask;
    private float[] _fillAmounts;

    public void Initialize() {
        int enemyCounts = _enemies.Length;
        for (int i = 0; i < enemyCounts; ++i) {
            _enemies[i].Initialize();
        }
        _fillAmounts = new float[4];
    }

    public Entity GetSelectedEnemy(Vector3 touchPos) {
        Entity target = null;
        int enemyCounts = _enemies.Length;
        for (int i = 0; i < enemyCounts; ++i) {
            if (_enemies[i].IsOverlapped(touchPos, _layerMask)) {
                target = _enemies[i];
            }
        }
        return target;
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i <  _enemies.Length; ++i) {
            _fillAmounts[i] =  _enemies[i].GetHPPercent(); 
        }
        return _fillAmounts;
    }
}
