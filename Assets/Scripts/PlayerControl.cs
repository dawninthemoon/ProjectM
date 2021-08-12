using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    [SerializeField] CardDeck _cardDeck = null;
    List<Card> _cardsInHand;
    public int CurrentCost { get; set; }

    public void Initialize() {
        _cardDeck.Initialize();
        _cardsInHand = new List<Card>();
    }
    
    public void DrawCard(int amount, bool turnStart = false) {
        if (turnStart) {
            amount = Mathf.Min(amount, _cardDeck.GetDeckCount());
        }

        var cardManager = CardManager.GetInstance();
        for (int i = 0; i < amount; ++i) {
            CardInfo cardInfo = _cardDeck.DrawCard();
            Card cardObj = cardManager.CreateCard(cardInfo);
            _cardsInHand.Add(cardObj);
        }

        CardManager.GetInstance().AlignCard(_cardsInHand);
        CardManager.GetInstance().SetOrder(_cardsInHand);
    }
}
