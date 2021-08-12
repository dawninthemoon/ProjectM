using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    [SerializeField] EntityInfo _info = null;
    public void Initialize(EntityInfo info) {
        _info = info;
    }
}
