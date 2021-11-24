using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyPair = System.Collections.Generic.KeyValuePair<int, int>;
using RieslingUtils;
using DG.Tweening;

public class PlayerControl : MonoBehaviour {
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int[] _characterKeys = null;
    [SerializeField] private Vector3[] _initialPosition = null;
    private List<CharacterEntity> _currentCharacters;
    [SerializeField] private SkillDeck _cardDeck = null;
    private List<Skill> _skillsInHand;
    public List<Skill> SkillsInHand { get { return _skillsInHand;} }
    public int CurrentCost { get; set; }
    private float[] _fillAmounts;
    private static readonly string CharacterEntityPrefabPath = "CharacterEntityPrefab";

    public void Initialize() {
        _currentCharacters = new List<CharacterEntity>();
        int characterCounts = _characterKeys.Length;
        List<KeyPair> skillInfoList = new List<KeyPair>();

        for (int i = 0; i < characterCounts; ++i) {
            CreateCharacterEntity(i);
            var parser = Data.SkillDataParser.Instance;

            string skillKey1 = _currentCharacters[i].CharacterData.SkillCard1Key;
            skillKey1 = skillKey1.TrimStart('[');
            skillKey1 = skillKey1.TrimEnd(']');
            var keys = skillKey1.Split(',');
            foreach (string key in keys) {
                AddSkillInfo(skillInfoList, key, _characterKeys[i]);
            }
        }

        _cardDeck.Initialize(skillInfoList);
        _skillsInHand = new List<Skill>();
        _fillAmounts = new float[3];
    }

    private void AddSkillInfo(List<KeyPair> skillInfoList, string skillKey, int characterKey) {
        if (skillKey != null) {
            KeyPair skillInfo = new KeyPair(int.Parse(skillKey), characterKey);
            skillInfoList.Add(skillInfo);
        }
    }

    private void CreateCharacterEntity(int index) {
        int key = _characterKeys[index];
        var characterData = Data.CharacterDataParser.Instance.GetCharacter(key);
        var characterStatData = Data.CharacterStatDataParser.Instance.GetCharacterStat(characterData.Key);
        
        var prefab = ResourceManager.GetInstance().GetEntityPrefab(CharacterEntityPrefabPath);
        _currentCharacters.Add(Instantiate(prefab, _initialPosition[index], Quaternion.identity) as CharacterEntity);

        _currentCharacters[index].transform.SetParent(transform);
        _currentCharacters[index].Initialize(characterData, characterStatData);
    }

    public void Progress() {
        for (int i = 0; i < _currentCharacters.Count; ++i) {
             if (_currentCharacters[i].CanRemoveEntity) {
                StartCoroutine(RemoveCharacter(_currentCharacters[i]));
                --i;
                continue;
            }
            _currentCharacters[i].Progress();
        }
    }

    public bool IsDefeated() {
        int characterCounts = 0;
        foreach (var character in _currentCharacters) {
            if (character.CurHP > 0) {
                ++characterCounts;
            }
        }
        return (characterCounts == 0);
    }

    public void RefreshCost() {
        CurrentCost = _currentCharacters[0].CharacterStatData.MaxCost;
    }

    public bool CanUseSkill(int requireCost) => (CurrentCost >= requireCost);
    public int GetMaxCost()  {
        return (_currentCharacters.Count > 0) ? _currentCharacters[0].CharacterStatData.MaxCost : 0;
    }

    public void DrawCard(bool turnStart = false) {
        int amount = Mathf.Min(_currentCharacters[0].CharacterStatData.MaxDraw - _skillsInHand.Count, _cardDeck.GetDeckCount());

        var cardManager = SkillManager.GetInstance();
        for (int i = 0; i < amount; ++i) {
            Data.SkillInfo skillData = _cardDeck.DrawCard();
            Skill cardObj = cardManager.CreateCard(skillData);
            _skillsInHand.Add(cardObj);
        }

        SkillManager.GetInstance().AlignCard(_skillsInHand);
        SkillManager.GetInstance().SetOrder(_skillsInHand);
    }

    public void UseSkill(Skill skill, BattleControl battleControl) {
        CurrentCost -= skill.GetRequireCost();

        Data.SkillInfo skillInfo = skill.GetSkillInfo();

        CharacterEntity caster = null;
        var casterData = Data.CharacterDataParser.Instance.GetCharacter(skillInfo.CharacterKey);
        for (int i = 0; i < _currentCharacters.Count; ++i) {
            if (_currentCharacters[i].KeyEquals(casterData.Key)) {
                caster = _currentCharacters[i];
                break;
            }
        }

        StartCoroutine(Act());
        IEnumerator Act() {
            Camera.main.DOOrthoSize(7.4f, 0.3f).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(0.5f);
            DoAttack(battleControl, skillInfo, caster);
            DoHeal(battleControl, skillInfo);
            ApplyAnimation(skillInfo, casterData, caster);
        }

        _cardDeck.SkillToGrave(skill.GetSkillInfo());
    }

    private void ApplyAnimation(Data.SkillInfo skillInfo, Data.Character casterData, CharacterEntity caster) {
        caster.ChangeAnimationState("Attack", false, () => caster.ChangeAnimationState("Idle", true));
        caster.SetAnimationDelay(1f);
    }

    private void DoAttack(BattleControl battleControl, Data.SkillInfo skillInfo, CharacterEntity caster) {
        Data.CharacterStat stat = Data.CharacterStatDataParser.Instance.GetCharacterStat(skillInfo.CharacterKey);

        var data = skillInfo.SkillData;
        float criticalDamage = MathUtils.GetPerTenThousand(stat.CriticalDamage);
        float baseDamage = stat.AttackPower * (1f + criticalDamage);
        int targetCounts = data.AttackTypeValue;
        var selectedTarget = battleControl.SelectedTarget;
        int finalDamage = 0;

        switch (data.AttackType) {
        case Data.AttackType.SingleAttack:
            finalDamage = Mathf.FloorToInt(baseDamage / selectedTarget.GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
            AttackTarget(selectedTarget, finalDamage);
            break;
        case Data.AttackType.ComboAttack:
            if (data.AttackType == Data.AttackType.ComboAttack) {
                finalDamage = Mathf.FloorToInt(baseDamage / selectedTarget.GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
                AttackTarget(selectedTarget, finalDamage);
            }
            var eList = battleControl.EnemyCtrl.GetMonsterByOrder(targetCounts, selectedTarget);
            foreach (var e in eList) {
                finalDamage = Mathf.FloorToInt(baseDamage / e.GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
                AttackTarget(e, finalDamage);
            }
            break;
        case Data.AttackType.MultiAttack:
            var enemies = battleControl.EnemyCtrl.GetAllMonsters();
            foreach (var e in enemies) {
                finalDamage = Mathf.FloorToInt(baseDamage / e.GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
                AttackTarget(e, finalDamage);
            }
            break;
        case Data.AttackType.RandomAttack:
            var enemy = battleControl.EnemyCtrl.GetRandomMonsters(targetCounts, selectedTarget);
            finalDamage = Mathf.FloorToInt(baseDamage / enemy[0].GetFinalDefence() * MathUtils.GetPerTenThousand(data.AttackRatio));
            AttackTarget(enemy[0], finalDamage);
            break;
        }

        void AttackTarget(BattleEntity entity, int amount) {
            entity.DecreaseHP(amount);
            entity.MoveForward(0.2f);
        }

        if (data.AttackType != Data.AttackType.None) {
            caster.MoveForward(1f);
        }

    }

    private void DoHeal(BattleControl battleControl, Data.SkillInfo skillInfo) {
        Data.CharacterStat stat = Data.CharacterStatDataParser.Instance.GetCharacterStat(skillInfo.CharacterKey);

        var data = skillInfo.SkillData;
        int targetCounts = skillInfo.SkillData.HealTypeValue;
        var selectedTarget = battleControl.SelectedTarget;
        int finalHeal = (int)MathUtils.GetPerTenThousand(data.HealRatio) * stat.AttackPower;

        switch (skillInfo.SkillData.HealType) {
        case Data.HealType.SingleHeal:
            HealTarget(selectedTarget, finalHeal);
            break;
        case Data.HealType.ComboHeal:
            var cList = GetCharacterByOrder(targetCounts, selectedTarget);
            foreach (var ch in cList) {
                HealTarget(ch, finalHeal);
            }
            break;
        case Data.HealType.MultiHeal:
            var characters = GetRandomCharacters(targetCounts, selectedTarget);
            foreach (var ch in characters) {
                HealTarget(ch, finalHeal);
            }
            break;
        case Data.HealType.RandomHeal:
            var character = GetAllCharacters();
            HealTarget(character[0], finalHeal);
            break;
        }

        void HealTarget(BattleEntity target, int amount) {
            target.IncreaseHP(amount);
        }
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i < _currentCharacters.Count; ++i) {
            _fillAmounts[i] = _currentCharacters[i].GetHPPercent(); 
        }
        return _fillAmounts;
    }

    public CharacterEntity GetSelectedCharacter(Vector3 touchPos) {
        CharacterEntity target = null;
        int enemyCounts = _currentCharacters.Count;
        for (int i = 0; i < enemyCounts; ++i) {
            if (_currentCharacters[i].IsOverlapped(touchPos, _layerMask)) {
                target = _currentCharacters[i];
            }
        }
        return target;
    }

    public List<CharacterEntity> GetCharacterByOrder(int targetCounts, BattleEntity ignoreEntity = null) {
        int numOfAllies = _currentCharacters.Count;
        int startIndex = 0;
        List<CharacterEntity> characterList = new List<CharacterEntity>();
        
        for (int i = 0; i < numOfAllies; ++i) {
            if (ignoreEntity == _currentCharacters[i]) {
                startIndex = i;
                break;
            }
        }

        for (int i = startIndex; i < targetCounts; i = (i + 1) % numOfAllies) {
            if (_currentCharacters[i] == ignoreEntity) break;
            characterList.Add(_currentCharacters[i]);
            
        }

        return characterList;
    }

    public List<CharacterEntity> GetAllCharacters() {
        return _currentCharacters;
    }

    public List<CharacterEntity> GetRandomCharacters(int targetCounts, BattleEntity ignoreEntity = null) {
        var characterList = _currentCharacters.ToList();
        for (int i = 0; i < characterList.Count; ++i) {
            if (characterList[i].CurHP <= 0) {
                characterList.RemoveAt(i--);
            }
        }
        int numOfAllies = characterList.Count;

        if (ignoreEntity) {
            characterList.Remove(ignoreEntity as CharacterEntity);
        }

        if (targetCounts < numOfAllies) {
            int diff = Mathf.Max(0, numOfAllies - targetCounts);
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, characterList.Count);
                characterList.RemoveAt(removeIndex);
            }
        }
        return characterList;
    }

    public IEnumerator RemoveCharacter(CharacterEntity character) {
        character.CanRemoveEntity = false;
        character.ChangeAnimationState("Dead");

        while (!character.IsAnimationEnd) {
            yield return null;
        }

        _currentCharacters.Remove(character);
        character.gameObject.SetActive(false);
    }
}
