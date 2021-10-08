using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

[System.Serializable]
public class SkillDeck {
    List<SkillInfo> _cardsInDeck;
    List<SkillInfo> _cardsInGrave;

    public void Initialize(List<KeyValuePair<int, int>> skillKeyPairList) {
        int skillCounts = skillKeyPairList.Count;
        _cardsInDeck = new List<SkillInfo>(skillCounts);

        foreach (var pair in skillKeyPairList) {
            SkillData skillData = Data.SkillDataParser.Instance.GetSkillData(pair.Key);
            SkillInfo skillInfo = new SkillInfo(skillData, pair.Value);
            _cardsInDeck.Add(skillInfo);
        }

        _cardsInGrave = new List<SkillInfo>();
    }

    public int GetDeckCount() => _cardsInDeck.Count;

    public SkillInfo DrawCard() {
        SkillInfo card = _cardsInDeck[0];
        _cardsInDeck.RemoveAt(0);

        if (_cardsInDeck.Count == 0) {
            var temp = _cardsInDeck;
            _cardsInDeck = _cardsInGrave;
            _cardsInGrave = _cardsInDeck;
        }

        return card;
    }

    public void SkillToGrave(SkillInfo skill) {
        _cardsInGrave.Add(skill);
    }
}