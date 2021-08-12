using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardInfo {
    public int _requireCost;
    public int _damage;
    public int _characterID;

    public CardInfo(int cost, int damage, int characterID) {
        _requireCost = cost;
        _damage = damage;
        _characterID = characterID;
    }
}