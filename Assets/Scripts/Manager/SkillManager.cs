using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillState {
    NOTHING,
    CARD_OVER,
    CARD_DRAG,
}
public class SkillManager : SingletonWithMonoBehaviour<SkillManager> {
    public SkillState State { get; set; } = SkillState.NOTHING;
    static readonly string SortingLayerName = "Cards";
    Vector3 _createPosition;
    static readonly Vector3 MiddlePosition = new Vector3(0.85f, -3.56f);

    protected override void Awake() {
        base.Awake();
        var deckUI = GameObject.Find("DeckButton");
        _createPosition = deckUI.transform.position;
    }
    public Skill CreateCard(SkillInfo info) {
        var prefab = ResourceManager.GetInstance().GetCardPrefab();
        var card = Instantiate(prefab, _createPosition, Quaternion.identity);
        card.Initialize(info);
        return card;
    }

    public void SetOrder(List<Skill> cards) {
        int curOrder = 0;
        int cardCounts = cards.Count;
        for (int i = 0; i < cardCounts; ++i) {
            cards[i].SprRenderer.sortingLayerName = SortingLayerName;
            cards[i].SetOrder(++curOrder);
        }
    }

    public void AlignCard(List<Skill> cards) {
        int cardCounts = cards.Count;
        if (cardCounts == 0) return;
        
        Vector3 cardOrigin = MiddlePosition;
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

    public void EnlargeCard(bool isEnlarge, Skill card) {
        if (isEnlarge) {
            Vector3 enLargePos = new Vector3(card.OriginPRS.pos.x, card.OriginPRS.pos.y + 1f, -10f);
            card.MoveTransform(new PRS(enLargePos, Quaternion.identity, Vector3.one * 1.3f), false);
        }
        else {
            card.MoveTransform(card.OriginPRS, false);
        }
        card.SetMostFrontOrder(isEnlarge);
    }
}
