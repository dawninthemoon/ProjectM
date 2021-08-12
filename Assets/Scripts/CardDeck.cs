using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardDeck {
    [SerializeField] List<CardInfo> _cardList = null;
    List<CardInfo> _cardsInDeck;
    List<CardInfo> _cardsInGrave;

    public void Initialize() {
        _cardsInDeck = _cardList.ConvertAll(o => o);
        _cardsInGrave = new List<CardInfo>();
    }

    public int GetDeckCount() => _cardsInDeck.Count;

    public CardInfo DrawCard() {
        CardInfo card = _cardsInDeck[0];
        _cardsInDeck.RemoveAt(0);

        if (_cardsInDeck.Count == 0) {
            var temp = _cardsInDeck;
            _cardsInDeck = _cardsInGrave;
            _cardsInGrave = _cardsInDeck;
        }

        return card;
    }
}