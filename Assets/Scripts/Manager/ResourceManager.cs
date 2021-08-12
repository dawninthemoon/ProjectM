using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager> {
    static readonly string PrefabPath = "Prefabs/";

    public Card GetCardPrefab() {
        string path = PrefabPath + "CardPrefab";
        var card = Resources.Load<Card>(path);
        return card;
    }
}
