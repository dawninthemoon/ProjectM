using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascot : MonoBehaviour {
    [SerializeField] MascotInfo _info = null;

    public int GetDrawAmount() => _info.draw;
}
