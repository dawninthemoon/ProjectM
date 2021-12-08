using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RieslingUtils;
using DG.Tweening;

public class MonsterControl : EntityControl {
    public override void Initialize() {
        _entityPrefabPath = "MonsterEntityPrefab/";
        _fillAmounts = new float[5];

        var stage = Data.BattleStageDataParser.Instance.FindStage("1");
        int[] monsterKeys = stage.Round1_Monster;

        int monsterCounts = monsterKeys.Length;
        _currentEntities = new List<BattleEntity>(monsterCounts);

        for (int i = 0; i < monsterCounts; ++i) {
            var monsterData = Data.MonsterDataParser.Instance.GetMonster(monsterKeys[i]);
            var prefab = ResourceManager.GetInstance().GetEntityPrefab(StringUtils.MergeStrings(_entityPrefabPath, monsterData.MonsterPrefab));

            MonsterEntity monster = Instantiate(prefab, _initialPosition[i], Quaternion.identity) as MonsterEntity;
            _currentEntities.Add(monster);
            monster.Initialize(monsterKeys[i], i);
        }
    }

    public IEnumerator UseSkill(BattleControl battleControl, System.Action uiSetupCallback) {
        foreach (MonsterEntity monster in _currentEntities) {
            if (battleControl.PlayerCtrl.IsDefeated()) break;
            
            Camera.main.DOOrthoSize(7.4f, 0.3f).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(0.5f);

            Data.SkillData skillData = monster.GetCurrentSkillData();
            if (skillData == null) continue;
            Data.SkillInfo skillInfo = new Data.SkillInfo(skillData, monster.Key);

            int grade = (int)Data.MonsterDataParser.Instance.GetMonster(skillInfo.CharacterKey).Level;
            SharedStat stat = new SharedStat(Data.MonsterStatDataParser.Instance.GetMonsterStat(grade));

            DoAttack(battleControl.SelectedTarget, battleControl.PlayerCtrl, skillInfo, stat, monster);
            DoHeal(battleControl.SelectedTarget, skillInfo, stat);

            string name = Data.MonsterDataParser.Instance.GetMonster(skillInfo.CharacterKey).Name;
            Debug.Log(name + "이 " + skillInfo.SkillData.Name + "을 사용!");
            monster.ChangeAnimationState("Attack");

            uiSetupCallback.Invoke();

            while (!monster.IsAnimationEnd) {
                yield return null;
            }

            monster.ChangeAnimationState("Idle", true);
            monster.SetAnimationDelay(1f);
        }
    }
}
