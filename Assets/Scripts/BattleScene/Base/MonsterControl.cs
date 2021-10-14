using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour {
    [SerializeField] MonsterEntity[] _monsters = null;
    [SerializeField] LayerMask _layerMask;
    private float[] _fillAmounts;

    public void Initialize() {
        _fillAmounts = new float[4];

        int enemyCounts = _monsters.Length;
        for (int i = 0; i < enemyCounts; ++i) {
            _monsters[i].Initialize();
        }
    }

    public void Progress() {
        for (int i = 0; i < _monsters.Length; ++i) {
            _monsters[i].Progress();
        }
    }

    public MonsterEntity GetSelectedEnemy(Vector3 touchPos) {
        MonsterEntity target = null;
        int enemyCounts = _monsters.Length;
        for (int i = 0; i < enemyCounts; ++i) {
            if (_monsters[i].IsOverlapped(touchPos, _layerMask)) {
                target = _monsters[i];
            }
        }
        return target;
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i <  _monsters.Length; ++i) {
            _fillAmounts[i] =  _monsters[i].GetHPPercent(); 
        }
        return _fillAmounts;
    }

    public List<MonsterEntity> GetMonsterByOrder(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _monsters.Length;
        int startIndex = 0;
        List<MonsterEntity> characterList = new List<MonsterEntity>();
        
        for (int i = 0; i < numOfAllies; ++i) {
            if (ignoreEntity == _monsters[i]) {
                startIndex = (i + 1) % numOfAllies;
                break;
            }
        }

        for (int i = startIndex; i < targetCounts; i = (i + 1) % numOfAllies) {
            if (_monsters[i] == ignoreEntity) break;
            characterList.Add(_monsters[i]);
            Debug.Log(i);
            
        }

        return characterList;
    }

    public List<MonsterEntity> GetAllMonsters() {
        return _monsters.ToList();
    }

    public List<MonsterEntity> GetRandomMonsters(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _monsters.Length;
        var enemyList = _monsters.ToList();
        
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
