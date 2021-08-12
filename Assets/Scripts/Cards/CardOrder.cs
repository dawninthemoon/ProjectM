using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOrder : MonoBehaviour {
    static readonly string SortingLayerName = "Cards";
    int _originOrder;

    public void SetOriginOrder(List<Card> cards, int originOrder) {
        _originOrder = originOrder;
        SetOrder(cards, originOrder);
    }

    /*
    public void SetMostFrontOrder(bool isMostFront) {
        SetOrder(isMostFront ? 100 : _originOrder);
    }
    */

    public void SetOrder(List<Card> cards, int order) {
        int curOrder = order * 10;
        int cardCounts = cards.Count;
        for (int i = 0; i < cardCounts; ++i) {
            cards[i].SprRenderer.sortingLayerName = SortingLayerName;
            cards[i].SprRenderer.sortingOrder = ++curOrder;
        }
    }

    public void AlignCard(List<Card> cards) {
        int cardCounts = cards.Count;
        if (cardCounts == 0) return;
        
        Vector3 cardOrigin = new Vector3(Camera.main.pixelWidth / 2f, 0f);
        cardOrigin = Camera.main.ScreenToWorldPoint(cardOrigin);
        cardOrigin.z = 0f;

        // x - width / 2, x + width / 2
        // x - width, x, x + width

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
}
