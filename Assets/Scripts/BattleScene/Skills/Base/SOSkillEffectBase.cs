using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOSkillEffectBase : ScriptableObject, ISkillEffect {
    public abstract void ExecuteSkill(SkillInfo info, BattleControl battleControl);
}
