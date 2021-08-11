using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOrder : MonoBehaviour {
    static readonly string SortingLayerName = "Cards";
    [SerializeField] SpriteRenderer[] _renderers = null;
    int _originOrder;

    public void SetOriginOrder(int originOrder) {
        _originOrder = originOrder;
        SetOrder(originOrder);
    }

    public void SetMostFrontOrder(bool isMostFront) {
        SetOrder(isMostFront ? 100 : _originOrder);
    }

    public void SetOrder(int order) {
        int curOrder = order * 10;
        int cardCounts = _renderers.Length;
        for (int i = 0; i < cardCounts; ++i) {
            _renderers[i].sortingLayerName = SortingLayerName;
            _renderers[i].sortingOrder = ++curOrder;
        }
    }
}
