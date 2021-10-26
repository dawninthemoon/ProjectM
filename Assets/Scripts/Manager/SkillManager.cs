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
    private static readonly string SortingLayerName = "Cards";
    private static readonly Vector3 MiddlePosition = new Vector3(0.85f, -3.56f);
    private Vector3 _createPosition;
    private SpriteRenderer _aimRenderer;
    private ObjectPool<Skill> _skillObjectPool;

    protected override void Awake() {
        base.Awake();
        var deckUI = GameObject.Find("DeckButton");
        _createPosition = deckUI.transform.position;

        _aimRenderer = gameObject.AddComponent<SpriteRenderer>();
        SetActiveAimSprite(false);
        _aimRenderer.sortingLayerName = "UI";
        _aimRenderer.sortingOrder = 10;
        _aimRenderer.sprite = ResourceManager.GetInstance().GetSprite("Sprites/Aim");

        _skillObjectPool = new ObjectPool<Skill>(
            10,
            CreateCard,
            (Skill card) => card.gameObject.SetActive(true),
            (Skill card) => card.gameObject.SetActive(false)
        );
    }

    private Skill CreateCard() {
        var prefab = ResourceManager.GetInstance().GetSkillPrefab();
        var card = Instantiate(prefab, _createPosition, Quaternion.identity);
        card.gameObject.SetActive(false);
        return card;
    }

    public Skill CreateCard(SkillInfo info) {
        Skill card = _skillObjectPool.GetObject();
        card.Initialize(info);
        return card;
    }

    public void ReturnCard(Skill card) {
        _skillObjectPool.ReturnObject(card);
    }

    public void SetOrder(List<Skill> cards) {
        int curOrder = 0;
        int cardCounts = cards.Count;
        for (int i = 0; i < cardCounts; ++i) {
            cards[i].SetOrder(SortingLayerName, ++curOrder);
        }
    }

    public void SetActiveAimSprite(bool enable) {
        _aimRenderer.gameObject.SetActive(enable);
    }

    public void SetAimPosition(Vector3 position) {
        _aimRenderer.transform.position = position;
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
