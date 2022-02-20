using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PromotionMaterial
{
    public int BaseGrade;
    public int Count;
    public PromotionInfo.PromotionMaterialType PromotionMaterialType;
    public int RequestGrade;
    public int ToGrade;
    public PromotionMaterial(int baseGrade, int count, PromotionInfo.PromotionMaterialType promotionMaterialType, int requestGrade, int toGrade)
    {
        BaseGrade = baseGrade;
        Count = count;
        PromotionMaterialType = promotionMaterialType;
        RequestGrade = requestGrade;
        ToGrade = toGrade;
    }
}

public enum SpiritStartGradeType
{
    Common = 10,
    Rare = 20,
    Epic = 30
}
public static class PromotionInfo
{
    public enum PromotionMaterialType
    {
        Diffrent,
        Same
    }
    public const int COMMON = 10;
    public const int UNCOMMON = 11;

    public const int RARE = 20;

    public const int EPIC = 30;
    public const int EPIC1 = 31;
    public const int EPIC2 = 32;

    public const int UNIQUE = 40;
    public const int UNIQUE1 = 41;
    public const int UNIQUE2 = 42;
    public const int UNIQUE3 = 43;

    public const int LEGENDRY = 50;
    public const int LEGENDRY1 = 51;
    public const int LEGENDRY2 = 52;
    public const int LEGENDRY3 = 53;
    public const int LEGENDRY4 = 54;

    public const int GOD = 60;
    public const int GOD1 = 61;
    public const int GOD2 = 62;
    public const int GOD3 = 63;
    public const int GOD4 = 64;
    public const int GOD5 = 65;

    public static readonly Color COMMON_COLOR = Color.gray;
    public static readonly Color UNCOMMON_COLOR = Color.blue;
    public static readonly Color RARE_COLOR = Color.cyan;
    public static readonly Color EPIC_COLOR = new Color(138 / 255f, 57 / 255f, 1);
    public static readonly Color UNIQUE_COLOR = Color.yellow;
    public static readonly Color LEGENDRY_COLOR = Color.green;
    public static readonly Color GOD_COLOR = Color.red;

    public static Color GetGradeColor(int grade)
    {
        switch(grade)
        {
            case COMMON: 
                return COMMON_COLOR;
            case UNCOMMON: 
                return UNCOMMON_COLOR;
            case RARE:
                return RARE_COLOR;
            case EPIC:
            case EPIC1:
            case EPIC2:
                return EPIC_COLOR;
            case UNIQUE:
            case UNIQUE1:
            case UNIQUE2:
            case UNIQUE3:
                return UNIQUE_COLOR;
            case LEGENDRY:
            case LEGENDRY1:
            case LEGENDRY2:
            case LEGENDRY3:
            case LEGENDRY4:
                return LEGENDRY_COLOR;
            case GOD:
            case GOD1:
            case GOD2:
            case GOD3:
            case GOD4:
            case GOD5:
                return GOD_COLOR;
        }
        return COMMON_COLOR;
    }

    public static PromotionMaterial[] PromotionMaterials =
        new PromotionMaterial[]
        {
            new PromotionMaterial(COMMON, 2, PromotionMaterialType.Same, COMMON, UNCOMMON),
            new PromotionMaterial(UNCOMMON, 2, PromotionMaterialType.Same, UNCOMMON, RARE),

            new PromotionMaterial(RARE, 2, PromotionMaterialType.Same, RARE, EPIC),

            new PromotionMaterial(EPIC, 1, PromotionMaterialType.Diffrent, EPIC, EPIC1),
            new PromotionMaterial(EPIC1, 1, PromotionMaterialType.Diffrent, EPIC, EPIC2),
            new PromotionMaterial(EPIC2, 1, PromotionMaterialType.Same, EPIC, UNIQUE),

            new PromotionMaterial(UNIQUE, 1, PromotionMaterialType.Diffrent, UNIQUE, UNIQUE1),
            new PromotionMaterial(UNIQUE1, 1, PromotionMaterialType.Diffrent, UNIQUE, UNIQUE2),
            new PromotionMaterial(UNIQUE2, 1, PromotionMaterialType.Diffrent, UNIQUE, UNIQUE3),
            new PromotionMaterial(UNIQUE3, 1, PromotionMaterialType.Same, UNIQUE, LEGENDRY),

            new PromotionMaterial(LEGENDRY, 1, PromotionMaterialType.Diffrent, LEGENDRY, LEGENDRY1),
            new PromotionMaterial(LEGENDRY1, 1, PromotionMaterialType.Diffrent, LEGENDRY, LEGENDRY2),
            new PromotionMaterial(LEGENDRY2, 1, PromotionMaterialType.Diffrent, LEGENDRY, LEGENDRY3),
            new PromotionMaterial(LEGENDRY3, 1, PromotionMaterialType.Diffrent, LEGENDRY, LEGENDRY4),
            new PromotionMaterial(LEGENDRY4, 1, PromotionMaterialType.Same, LEGENDRY, GOD),

            new PromotionMaterial(GOD, 1, PromotionMaterialType.Diffrent, GOD, GOD1),
            new PromotionMaterial(GOD1, 1, PromotionMaterialType.Diffrent, GOD, GOD2),
            new PromotionMaterial(GOD2, 1, PromotionMaterialType.Diffrent, GOD, GOD3),
            new PromotionMaterial(GOD3, 1, PromotionMaterialType.Diffrent, GOD, GOD4),
            new PromotionMaterial(GOD4, 1, PromotionMaterialType.Diffrent, GOD, GOD5)
        };
}
