using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;

public enum SkillState {
    NOTHING,
    CARD_OVER,
    CARD_DRAG,
}
public class SkillManager : SingletonWithMonoBehaviour<SkillManager> {
    public SkillState State { get; set; } = SkillState.NOTHING;
    private static readonly string SortingLayerName = "Cards";
    private Vector2 _middlePosition;
    private Vector3 _createPosition;
    private SpriteRenderer _aimRenderer;
    private ObjectPool<Skill> _skillObjectPool;
    private Transform _cardTransform;

    public void Initialize(Camera cardCamera) {
        _cardTransform = GameObject.Find("[ Cards ]").transform;
        var deckUI = GameObject.Find("DeckButton");

        _createPosition = cardCamera.ScreenToWorldPoint(deckUI.transform.position);
        _middlePosition = cardCamera.transform.position;
        _middlePosition.y = -3.5f;

        _aimRenderer = gameObject.AddComponent<SpriteRenderer>();
        SetActiveAimSprite(false);
        _aimRenderer.gameObject.layer = LayerMask.NameToLayer("Card");
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
        card.transform.SetParent(_cardTransform);
        card.gameObject.SetActive(false);
        return card;
    }

    public Skill CreateCard(Data.SkillData skillData) {
        Skill card = _skillObjectPool.GetObject();
        card.transform.position = _createPosition;
        card.Initialize(skillData);
        return card;
    }

    public void ReturnCard(Skill card) {
        _skillObjectPool.ReturnObject(card);
    }

    public void SetOrder(List<Skill> cards) {
        int curOrder = 0;
        float defaultZ = 100f;
        int cardCounts = cards.Count;
        for (int i = 0; i < cardCounts; ++i) {
            cards[i].SetOrder(SortingLayerName, ++curOrder);
            var t = cards[i].transform;
            t.position = t.position.ChangeZPos(-curOrder + defaultZ);
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
        
        Vector3 cardOrigin = _middlePosition;
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
