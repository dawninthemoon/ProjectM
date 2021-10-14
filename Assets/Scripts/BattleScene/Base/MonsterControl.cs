using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour {
    [SerializeField] private int[] _monsterKeys = null;
    [SerializeField] private Vector3[] _initialPosition = null;
    [SerializeField] LayerMask _layerMask;
    private List<MonsterEntity> _currentMonsters;
    private float[] _fillAmounts;
    private static readonly string MonsterPreafabPath = "MonsterEntityPrefab/";

    public void Initialize() {
        _fillAmounts = new float[4];

        int monsterCounts = _monsterKeys.Length;
        _currentMonsters = new List<MonsterEntity>(monsterCounts);

        for (int i = 0; i < monsterCounts; ++i) {
            var monsterData = Data.MonsterDataParser.Instance.GetMonster(_monsterKeys[i]);
            var prefab = ResourceManager.GetInstance().GetEntityPrefab(MonsterPreafabPath + monsterData.MonsterPrefab);

            BattleEntity monster = Instantiate(prefab, _initialPosition[i], Quaternion.identity);
            _currentMonsters.Add(monster as MonsterEntity);
            _currentMonsters[i].Initialize(_monsterKeys[i], i);
        }
    }

    public void Progress() {
        for (int i = 0; i < _currentMonsters.Count; ++i) {
            _currentMonsters[i].Progress();
        }
    }

    public MonsterEntity GetSelectedEnemy(Vector3 touchPos) {
        MonsterEntity target = null;
        int monsterCounts = _currentMonsters.Count;
        for (int i = 0; i < monsterCounts; ++i) {
            if (_currentMonsters[i].IsOverlapped(touchPos, _layerMask)) {
                target = _currentMonsters[i];
            }
        }
        return target;
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i < _fillAmounts.Length; ++i) {
            _fillAmounts[i] = 0f;
        }
        for (int i = 0; i <  _currentMonsters.Count; ++i) {
            var monster = _currentMonsters[i];
            _fillAmounts[monster.Order] = monster.GetHPPercent(); 
        }
        return _fillAmounts;
    }

    public List<MonsterEntity> GetMonsterByOrder(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _currentMonsters.Count;
        int startIndex = 0;
        List<MonsterEntity> characterList = new List<MonsterEntity>();
        
        for (int i = 0; i < numOfAllies; ++i) {
            if (ignoreEntity == _currentMonsters[i]) {
                startIndex = (i + 1) % numOfAllies;
                break;
            }
        }

        for (int i = startIndex; i < targetCounts; i = (i + 1) % numOfAllies) {
            if (_currentMonsters[i] == ignoreEntity) break;
            characterList.Add(_currentMonsters[i]);
            
        }

        return characterList;
    }

    public List<MonsterEntity> GetAllMonsters() {
        return _currentMonsters.ToList();
    }

    public List<MonsterEntity> GetRandomMonsters(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _currentMonsters.Count;
        var enemyList = _currentMonsters.ToList();
        
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

    public void RemoveMonster(MonsterEntity monster) {
        _currentMonsters.Remove(monster);
        monster.gameObject.SetActive(false);
    }
}
