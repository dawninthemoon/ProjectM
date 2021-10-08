using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour {
    [SerializeField] MonsterEntity[] _enemies = null;
    [SerializeField] LayerMask _layerMask;
    private float[] _fillAmounts;

    public void Initialize() {
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

    public MonsterEntity GetSelectedEnemy(Vector3 touchPos) {
        MonsterEntity target = null;
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

    public List<MonsterEntity> GetRandomEnemies(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _enemies.Length;
        var enemyList = _enemies.ToList();
        
        if (ignoreEntity) {
            enemyList.Remove(ignoreEntity as MonsterEntity);
        }

        if (targetCounts < numOfAllies) {
            int diff = numOfAllies - targetCounts;
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, enemyList.Count);
                enemyList.RemoveAt(removeIndex);
            }
        }
        return enemyList;
    }
}