using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    [SerializeField] BattleEntity[] _enemies = null;
    [SerializeField] LayerMask _layerMask;
    private float[] _fillAmounts;
    private List<BattleEntity> _enemyList;

    public void Initialize() {
        _enemyList = new List<BattleEntity>();
        _fillAmounts = new float[4];

        int enemyCounts = _enemies.Length;
        for (int i = 0; i < enemyCounts; ++i) {
            _enemies[i].Initialize();
        }
    }

    public void Progress() {
        for (int i = 0; i < _enemies.Length; ++i) {
            _enemies[i].Progress();
        }
    }

    public BattleEntity GetSelectedEnemy(Vector3 touchPos) {
        BattleEntity target = null;
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

    public List<BattleEntity> GetRandomEnemies(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _enemies.Length;
        _enemyList = _enemies.ToList();
        
        if (ignoreEntity) {
            _enemyList.Remove(ignoreEntity);
        }

        if (targetCounts < numOfAllies) {
            int diff = numOfAllies - targetCounts;
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, _enemyList.Count);
                _enemyList.RemoveAt(removeIndex);
            }
        }
        return _enemyList;
    }
}
