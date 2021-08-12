using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : SingletonWithMonoBehaviour<CardManager> {
    static readonly string SortingLayerName = "Cards";
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

    public void SetOrder(List<Card> cards) {
        int curOrder = 0;
        int cardCounts = cards.Count;
        for (int i = 0; i < cardCounts; ++i) {
            cards[i].SprRenderer.sortingLayerName = SortingLayerName;
            cards[i].SetOrder(++curOrder);
        }
    }

    public void AlignCard(List<Card> cards) {
        int cardCounts = cards.Count;
        if (cardCounts == 0) return;
        
        Vector3 cardOrigin = new Vector3(Camera.main.pixelWidth / 2f, 0f);
        cardOrigin = Camera.main.ScreenToWorldPoint(cardOrigin);
        cardOrigin.z = 0f;

        if (cardCounts % 2 == 0) {
            cardOrigin.x += cards[0].CardWidth;
            cardOrigin.x -= cards[0].CardWidth * 2f * (cardCounts / 2);
        }
        else {
            cardOrigin.x -= cards[0].CardWidth * 2f * (cardCounts / 2);
        }

        for (int i = 0; i < cardCounts; ++i) {
            cards[i].OriginPRS = new PRS(cardOrigin, Quaternion.identity, Vector3.one);
            cards[i].MoveTransform(cards[i].OriginPRS, true, 0.7f);
            cardOrigin.x += cards[i].CardWidth * 2f;
        }
    }

    public void EnlargeCard(bool isEnlarge, Card card) {
        if (isEnlarge) {
            Vector3 enLargePos = new Vector3(card.OriginPRS.pos.x, -4.8f, -10f);
            card.MoveTransform(new PRS(enLargePos, Quaternion.identity, Vector3.one * 1.3f), false);
        }
        else {
            card.MoveTransform(card.OriginPRS, false);
        }
        card.SetMostFrontOrder(isEnlarge);
    }
}
