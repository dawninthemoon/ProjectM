using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOAttackRandom", menuName = "SkillEffect/AttackRandom")]
public class SOAttackRandom : SOSkillEffectBase {
    public override void ExecuteSkill(SkillInfo info, BattleControl battleControl) {
        var targets = battleControl.EnemyCtrl.GetRandomEnemies(info.numOfTargets);
        foreach (Entity target in targets) {
            target.DecreaseHP(info.value);
        }
    }
}