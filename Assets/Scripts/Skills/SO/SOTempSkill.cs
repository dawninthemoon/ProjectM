using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOTempSkill", menuName = "SkillEffect/TempSkill")]
public class SOTempSkill : SOSkillEffectBase {
    public override void ExecuteSkill(SkillInfo info, BattleControl battleControl) {
        var target = battleControl.SelectedTarget;
        target.DecreaseHP(info.value);
    }
}