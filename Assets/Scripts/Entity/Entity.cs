using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    EntityInfo _info;

    public void Initialize(EntityInfo info) {
        _info = info;
    }
}
