using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillEffect {
    void ExecuteSkill();
    void ExecuteSkill(Entity target);
}