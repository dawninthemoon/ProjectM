using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyPair = System.Collections.Generic.KeyValuePair<int, int>;

public class PlayerControl : MonoBehaviour {
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int[] _characterKeys = null;
    [SerializeField] private Mascot _mascot = null;
    [SerializeField] private Vector3[] _initialPosition = null;
    private CharacterEntity[] _characters = null;
    [SerializeField] private SkillDeck _cardDeck = null;
    private List<Skill> _skillsInHand;
    public List<Skill> SkillsInHand { get { return _skillsInHand;} }
    public int CurrentCost { get; set; }
    private float[] _fillAmounts;
    private static readonly string CharacterEntityPrefabPath = "CharacterEntityPrefab/";

    public void Initialize() {
        int characterCounts = _characterKeys.Length;
        _characters = new CharacterEntity[characterCounts];
        List<KeyPair> skillInfoList = new List<KeyPair>();

        for (int i = 0; i < characterCounts; ++i) {
            CreateCharacterEntity(i);

            var parser = Data.SkillDataParser.Instance;
            string skillKey1 = _characters[i].CharacterData.SkillCard1Key;
            string skillKey2 = _characters[i].CharacterData.SkillCard2Key;

            AddSkillInfo(skillInfoList, _characters[i].CharacterData.SkillCard1Key, _characterKeys[i]);
            AddSkillInfo(skillInfoList, _characters[i].CharacterData.SkillCard2Key, _characterKeys[i]);
        }

        _cardDeck.Initialize(skillInfoList);
        _skillsInHand = new List<Skill>();
        _fillAmounts = new float[3];
    }

    private void AddSkillInfo(List<KeyPair> skillInfoList, string skillKey, int characterKey) {
        if (skillKey != null) {
            KeyPair skillInfo = new KeyPair(int.Parse(skillKey), characterKey);
            skillInfoList.Add(skillInfo);
        }
    }

    private void CreateCharacterEntity(int index) {
        int key = _characterKeys[index];
        var characterData = Data.CharacterDataParser.Instance.GetCharacter(key);
        var characterStatData = Data.CharacterStatDataParser.Instance.GetCharacterStat(characterData.Key);
        
        string prefabName = characterData.SubName;
        var prefab = ResourceManager.GetInstance().GetEntityPrefab(CharacterEntityPrefabPath + prefabName);
        _characters[index] = Instantiate(prefab, _initialPosition[index], Quaternion.identity) as CharacterEntity;

        _characters[index].transform.SetParent(transform);
        _characters[index].Initialize(characterData, characterStatData);
    }

    public void Progress() {
        for (int i = 0; i < _characters.Length; ++i) {
            _characters[i].Progress();
        }
    }

    public void RefreshCost() {
        CurrentCost = _mascot.GetCostAmount();
    }

    public bool CanUseSkill(int requireCost) => (CurrentCost >= requireCost);
    public int GetMaxCost() => _mascot.GetCostAmount();
    
    public void DrawCard(bool turnStart = false) {
        int amount = _mascot.GetDrawAmount();
        if (turnStart) {
            amount = Mathf.Min(amount, _cardDeck.GetDeckCount());
        }

        var cardManager = SkillManager.GetInstance();
        for (int i = 0; i < amount; ++i) {
            Data.SkillInfo skillData = _cardDeck.DrawCard();
            Skill cardObj = cardManager.CreateCard(skillData);
            _skillsInHand.Add(cardObj);
        }

        SkillManager.GetInstance().AlignCard(_skillsInHand);
        SkillManager.GetInstance().SetOrder(_skillsInHand);
    }

    public void UseSkill(Skill skill, BattleControl battleControl) {
        CurrentCost -= skill.GetRequireCost();
        skill.UseSkill(battleControl);
        _cardDeck.SkillToGrave(skill.GetSkillInfo());
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i < _characters.Length; ++i) {
            _fillAmounts[i] = _characters[i].GetHPPercent(); 
        }
        return _fillAmounts;
    }

    public CharacterEntity GetSelectedCharacter(Vector3 touchPos) {
        CharacterEntity target = null;
        int enemyCounts = _characters.Length;
        for (int i = 0; i < enemyCounts; ++i) {
            if (_characters[i].IsOverlapped(touchPos, _layerMask)) {
                target = _characters[i];
            }
        }
        return target;
    }

    public List<CharacterEntity> GetCharacterByOrder(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _characters.Length;
        int startIndex = 0;
        List<CharacterEntity> characterList = new List<CharacterEntity>();
        
        for (int i = 0; i < numOfAllies; ++i) {
            if (ignoreEntity == _characters[i]) {
                startIndex = i;
                break;
            }
        }

        for (int i = startIndex; i < targetCounts; i = (i + 1) % numOfAllies) {
            if (_characters[i] == ignoreEntity) break;
            characterList.Add(_characters[i]);
            
        }

        return characterList;
    }

    public List<CharacterEntity> GetAllCharacters() {
        return _characters.ToList();
    }

    public List<CharacterEntity> GetRandomCharacter(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _characters.Length;
        var characterList = _characters.ToList();

        if (ignoreEntity) {
            characterList.Remove(ignoreEntity as CharacterEntity);
        }

        if (targetCounts < numOfAllies) {
            int diff = Mathf.Max(0, numOfAllies - targetCounts);
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, characterList.Count);
                characterList.RemoveAt(removeIndex);
            }
        }
        return characterList;
    }
}
