using Data;
using UnityEngine;
using UnityEngine.U2D;

public class SpiritIconSpriteControl : MonoBehaviour
{
    private static SpiritIconSpriteControl instance;

    public static SpiritIconSpriteControl Instance
    {
        get { return instance; }
    }

    [SerializeField] private SpriteAtlas spilitAtlas;

    public void Awake()
    {
        instance = this;
    }

    public Sprite GetSpiritSprite(int index)
    {
        SpiritData spiritData = SpiritDataParser.Instance.GetSpiritData(index);

        if (spiritData == null)
            return null;

        return spilitAtlas.GetSprite(spiritData.IconName);
    }
}