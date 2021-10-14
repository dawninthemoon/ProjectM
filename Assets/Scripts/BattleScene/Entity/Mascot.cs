using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascot : MonoBehaviour {
    [SerializeField] private MascotInfo _info = null;
    public int GetStartDrawAmount() => _info.startDraw;
    public int GetDrawAmount() => _info.draw;
    public int GetCostAmount() => _info.cost;
    public int GetSpeedAmount() => _info.speed;
}
