using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : SingletonWithMonoBehaviour<CardManager> {
    Vector3 _createPosition;
    protected override void Awake() {
        base.Awake();
        var deckUI = GameObject.Find("DeckButton");
        _createPosition = deckUI.transform.position;
    }
    public Card CreateCard(CardInfo info) {
        var prefab = ResourceManager.GetInstance().GetCardPrefab();
        var card = Instantiate(prefab, _createPosition, Quaternion.identity);
        card.Initialize(info);
        return card;
    }
}
