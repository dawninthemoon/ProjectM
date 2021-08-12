using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    [SerializeField] Mascot _mascot = null;
    [SerializeField] SkillDeck _cardDeck = null;
    List<Skill> _skillsInHand;
    public int CurrentCost { get; set; }

    public void Initialize() {
        _cardDeck.Initialize();
        _skillsInHand = new List<Skill>();
    }
    
    public void DrawCard(bool turnStart = false) {
        int amount = _mascot.GetDrawAmount();
        if (turnStart) {
            amount = Mathf.Min(amount, _cardDeck.GetDeckCount());
        }

        var cardManager = SkillManager.GetInstance();
        for (int i = 0; i < amount; ++i) {
            SkillInfo cardInfo = _cardDeck.DrawCard();
            Skill cardObj = cardManager.CreateCard(cardInfo);
            _skillsInHand.Add(cardObj);
        }

        SkillManager.GetInstance().AlignCard(_skillsInHand);
        SkillManager.GetInstance().SetOrder(_skillsInHand);
    }
}
