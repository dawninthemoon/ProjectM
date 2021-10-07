using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

[System.Serializable]
public class SkillDeck {
    List<SkillData> _cardsInDeck;
    List<SkillData> _cardsInGrave;

    public void Initialize() {
        _cardsInDeck = new List<SkillData>(17);
        for (int key = 1; key <= 17; ++key) {
            _cardsInDeck.Add(Data.SkillDataParser.Instance.GetSkillData(key));
        }
        _cardsInGrave = new List<SkillData>();
    }

    public int GetDeckCount() => _cardsInDeck.Count;

    public SkillData DrawCard() {
        SkillData card = _cardsInDeck[0];
        _cardsInDeck.RemoveAt(0);

        if (_cardsInDeck.Count == 0) {
            var temp = _cardsInDeck;
            _cardsInDeck = _cardsInGrave;
            _cardsInGrave = _cardsInDeck;
        }

        return card;
    }

    public void SkillToGrave(SkillData skill) {
        _cardsInGrave.Add(skill);
    }
}