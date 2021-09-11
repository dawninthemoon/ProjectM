using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOHealRandom", menuName = "SkillEffect/HealRandom")]
public class SOHealRandom : SOSkillEffectBase {
    public override void ExecuteSkill(SkillInfo info, BattleControl battleControl) {
        var targets = battleControl.PlayerCtrl.GetRandomAllies(info.numOfTargets);
        foreach (BattleEntity target in targets) {
            target.IncreaseHP(info.value);
        }
    }
}