using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOAttackTarget", menuName = "SkillEffect/AttackTarget")]
public class SOAttackTarget : SOSkillEffectBase {
    public override void ExecuteSkill(SkillInfo info, BattleControl battleControl) {
        var target = battleControl.SelectedTarget;
        target.DecreaseHP(info.value);

        if (info.numOfTargets > 1) {
            var targets = battleControl.EnemyCtrl.GetRandomEnemies(info.numOfTargets - 1, target);
            for (int i = 0; i < info.numOfTargets - 1; ++i) {
                targets[i].DecreaseHP(info.value);
            }
        }
    }
}