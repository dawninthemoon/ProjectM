using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOSkillEffectBase : ScriptableObject, ISkillEffect {
    public abstract void ExecuteSkill(BattleControl battleControl);
}
