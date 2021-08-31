using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RieslingUtils;

public struct BattleUIArgs {
    public float[] allyHPFillAmounts;
    public float[] enemyHPFillAmounts;
    public int currentCost;
    public int maxCost;

    public BattleUIArgs(float[] ally01, float[] enemy01, int curCost, int maxCost) {
        allyHPFillAmounts = ally01;
        enemyHPFillAmounts = enemy01;
        currentCost = curCost;
        this.maxCost = maxCost;
    }
}


public class BattleUI : MonoBehaviour {
    [SerializeField] Image[] _allyHPBarImages = null;
    [SerializeField] Image[] _allyHPUIImages = null;
    [SerializeField] Image[] _allyEXPUIImages = null;
    [SerializeField] Image[] _enemyHPBarImages = null;
    [SerializeField] TextMeshProUGUI _costText = null;
    private static readonly string SlashString = "/";

    public void OnSkillUsed(BattleUIArgs args) {
        OnAllyHPChanged(args.allyHPFillAmounts);
        OnEnemyHPChanged(args.enemyHPFillAmounts);
        OnCostChanged(args.currentCost, args.maxCost);
    }

    private void OnAllyHPChanged(float[] args) {
        for (int i = 0; i < _allyHPBarImages.Length; ++i) {
            if (args == null || args.Length <= i) {
                SetImageFillAmount(_allyHPBarImages[i], 1f);
                SetImageFillAmount(_allyHPUIImages[i], 1f);
            }
            else {
                SetImageFillAmount(_allyHPBarImages[i], args[i]);
                SetImageFillAmount(_allyHPUIImages[i], args[i]);
            }   
        }
    }

    private void OnEnemyHPChanged(float[] args) {
        for (int i = 0; i < _enemyHPBarImages.Length; ++i) {
            if (args == null || args.Length <= i) {
                SetImageFillAmount(_enemyHPBarImages[i], 1f);
            }
            else {
                SetImageFillAmount(_enemyHPBarImages[i], args[i]);
            }   
        }
    }

    private void OnCostChanged(int currentCost, int maxCost) {
        _costText.text = StringUtils.MergeStrings(currentCost.ToString(), SlashString, maxCost.ToString());
    }

    private void SetImageFillAmount(Image image, float amount) {
        image.fillAmount = amount;
    }
}
