using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOHealTarget", menuName = "SkillEffect/HealTarget")]
public class SOHealTarget : SOSkillEffectBase {
    public override void ExecuteSkill(SkillInfo info, BattleControl battleControl) {
        var target = battleControl.SelectedTarget;
        target.IncreaseHP(info.value);

        if (info.numOfTargets > 1) {
            var targets = battleControl.PlayerCtrl.GetRandomAllies(info.numOfTargets - 1, target);
            for (int i = 0; i < info.numOfTargets - 1; ++i) {
                targets[i].IncreaseHP(info.value);
            }
        }
    }
}