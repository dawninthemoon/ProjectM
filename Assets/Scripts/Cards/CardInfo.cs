using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardInfo {
    public int _requireCost;
    public int _damage;
    public bool _isTargeting;
    public int _characterID;

    public CardInfo(int cost, int damage, bool isTargeting) {
        _requireCost = cost;
        _damage = damage;
        _isTargeting = isTargeting;
    }
}