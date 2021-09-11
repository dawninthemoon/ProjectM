using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOHealSelf", menuName = "SkillEffect/HealSelf")]
public class SOHealSelf : SOSkillEffectBase {
    public override void ExecuteSkill(SkillInfo info, BattleControl battleControl) {
        var target = battleControl.SelectedTarget;
        target.IncreaseHP(info.value);
    }
}