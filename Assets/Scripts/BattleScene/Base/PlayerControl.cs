using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyPair = System.Collections.Generic.KeyValuePair<int, int>;

public class PlayerControl : MonoBehaviour {
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

    public List<CharacterEntity> GetRandomAllies(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _characters.Length;
        var allyList = _characters.ToList();

        if (ignoreEntity) {
            allyList.Remove(ignoreEntity as CharacterEntity);
        }

        if (targetCounts < numOfAllies) {
            int diff = numOfAllies - targetCounts;
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, allyList.Count);
                allyList.RemoveAt(removeIndex);
            }
        }
        return allyList;
    }
}
