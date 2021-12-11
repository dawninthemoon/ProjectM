using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyPair = System.Collections.Generic.KeyValuePair<int, int>;

public class PlayerControl : EntityControl
{
    [SerializeField] private int[] _characterKeys = null;
    [SerializeField] private SkillDeck _cardDeck = null;
    private List<Skill> _skillsInHand;

    public List<Skill> SkillsInHand
    { get { return _skillsInHand; } }

    public int CurrentCost { get; set; }

    public override void Initialize()
    {
        _fillAmounts = new float[3];
        _skillsInHand = new List<Skill>();

        _entityPrefabPath = "CharacterEntityPrefab";
        _currentEntities = new List<BattleEntity>();
        int characterCounts = _characterKeys.Length;
        List<KeyPair> skillInfoList = new List<KeyPair>();

        for (int i = 0; i < characterCounts; ++i)
        {
            CreateCharacterEntity(i);
            var parser = Data.SkillDataParser.Instance;

            CharacterEntity character = _currentEntities[i] as CharacterEntity;
            var keys = character.CharacterData.SkillCard1Key;
            foreach (string key in keys)
            {
                AddSkillInfo(skillInfoList, key, _characterKeys[i]);
            }
        }

        _cardDeck.Initialize(skillInfoList);
    }

    private void AddSkillInfo(List<KeyPair> skillInfoList, string skillKey, int characterKey)
    {
        if (skillKey != null)
        {
            KeyPair skillInfo = new KeyPair(int.Parse(skillKey), characterKey);
            skillInfoList.Add(skillInfo);
        }
    }

    private void CreateCharacterEntity(int index)
    {
        int key = _characterKeys[index];
        var characterData = Data.CharacterDataParser.Instance.GetCharacter(key);
        var characterStatData = Data.CharacterStatDataParser.Instance.GetCharacterStat(characterData.Key);

        var prefab = ResourceManager.GetInstance().GetEntityPrefab(_entityPrefabPath);

        CharacterEntity character = Instantiate(prefab, _initialPosition[index], Quaternion.identity) as CharacterEntity;
        _currentEntities.Add(character);

        character.transform.SetParent(transform);
        character.Initialize(characterData, characterStatData);
    }

    public bool IsDefeated()
    {
        int characterCounts = 0;
        foreach (var character in _currentEntities)
        {
            if (character.CurHP > 0)
            {
                ++characterCounts;
            }
        }
        return (characterCounts == 0);
    }

    public void RefreshCost()
    {
        CharacterEntity character = _currentEntities[0] as CharacterEntity;
        CurrentCost = character.CharacterStatData.MaxCost;
    }

    public bool CanUseSkill(int requireCost) => (CurrentCost >= requireCost);

    public int GetMaxCost()
    {
        int maxCost = 0;
        if (_currentEntities.Count > 0)
        {
            CharacterEntity character = _currentEntities[0] as CharacterEntity;
            maxCost = character.CharacterStatData.MaxCost;
        }
        return maxCost;
    }

    public void DrawCard(bool turnStart = false)
    {
        CharacterEntity character = _currentEntities[0] as CharacterEntity;
        int amount = Mathf.Min(character.CharacterStatData.MaxDraw - _skillsInHand.Count, _cardDeck.GetDeckCount());

        var cardManager = SkillManager.GetInstance();
        for (int i = 0; i < amount; ++i)
        {
            Data.SkillInfo skillData = _cardDeck.DrawCard();
            Skill cardObj = cardManager.CreateCard(skillData);
            _skillsInHand.Add(cardObj);
        }

        SkillManager.GetInstance().AlignCard(_skillsInHand);
        SkillManager.GetInstance().SetOrder(_skillsInHand);
    }

    public void UseSkill(Skill skill, BattleControl battleControl)
    {
        CurrentCost -= skill.GetRequireCost();

        Data.SkillInfo skillInfo = skill.GetSkillInfo();

        var casterData = Data.CharacterDataParser.Instance.GetCharacter(skillInfo.CharacterKey);
        CharacterEntity caster = FindEntityByKey(casterData.Key) as CharacterEntity;

        StartCoroutine(Act());
        _cardDeck.SkillToGrave(skill.GetSkillInfo());

        IEnumerator Act()
        {
            Camera.main.DOOrthoSize(7.4f, 0.3f).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(0.5f);

            SharedStat stat = new SharedStat(Data.CharacterStatDataParser.Instance.GetCharacterStat(skillInfo.CharacterKey));
            DoAttack(battleControl.SelectedTarget, battleControl.EnemyCtrl, skillInfo, stat, caster);
            DoHeal(battleControl.SelectedTarget, skillInfo, stat);
            ApplyAnimation(skillInfo, casterData, caster);
        }
    }

    private void ApplyAnimation(Data.SkillInfo skillInfo, Data.Character casterData, CharacterEntity caster)
    {
        caster.ChangeAnimationState("Attack", false, () => caster.ChangeAnimationState("Idle", true));
        caster.SetAnimationDelay(1f);
    }
}