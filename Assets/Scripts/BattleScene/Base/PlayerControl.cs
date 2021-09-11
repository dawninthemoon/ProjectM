using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    [SerializeField] Mascot _mascot = null;
    [SerializeField] BattleEntity[] _allies = null;
    [SerializeField] SkillDeck _cardDeck = null;
    List<Skill> _skillsInHand;
    public List<Skill> SkillsInHand { get { return _skillsInHand;} }
    public int CurrentCost { get; set; }
    private float[] _fillAmounts;
    private List<BattleEntity> _allyList;

    public void Initialize() {
        _cardDeck.Initialize();
        _skillsInHand = new List<Skill>();
        _allyList = new List<BattleEntity>(4);
        _fillAmounts = new float[4];

        for (int i = 0; i < _allies.Length; ++i) {
            _allies[i].Initialize();
        }
    }

    public void Progress() {
        for (int i = 0; i < _allies.Length; ++i) {
            _allies[i].Progress();
        }
    }

    public void RefreshCost() {
        CurrentCost = _mascot.GetCostAmount();
    }

    public bool CanUseSkill(int requireCost) => (CurrentCost >= requireCost);
    public int GetMaxCost() => _mascot.GetCostAmount();
    
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

    public void UseSkill(Skill skill, BattleControl battleControl) {
        CurrentCost -= skill.GetRequireCost();
        skill.UseSkill(battleControl);
        _cardDeck.SkillToGrave(skill.GetSkillInfo());
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i < _allies.Length; ++i) {
            _fillAmounts[i] = _allies[i].GetHPPercent(); 
        }
        return _fillAmounts;
    }

    public List<BattleEntity> GetRandomAllies(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _allies.Length;
        _allyList = _allies.ToList();

        if (ignoreEntity) {
            _allyList.Remove(ignoreEntity);
        }

        if (targetCounts < numOfAllies) {
            int diff = numOfAllies - targetCounts;
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, _allyList.Count);
                _allyList.RemoveAt(removeIndex);
            }
        }
        return _allyList;
    }
}