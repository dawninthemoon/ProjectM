using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillEffect {
    void ExecuteSkill(SkillInfo info, BattleControl battleControl);
}