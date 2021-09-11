using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDeck {
    [SerializeField] List<SkillInfo> _cardList = null;
    List<SkillInfo> _cardsInDeck;
    List<SkillInfo> _cardsInGrave;

    public void Initialize() {
        _cardsInDeck = _cardList.ConvertAll(o => o);
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