using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] Image[] _allyHPBarImage = null;
    [SerializeField] Image[] _enemyHPBarImage = null;
    [SerializeField] TextMeshProUGUI _costText = null;
    private static readonly string SlashString = "/";

    public void OnSkillUsed(BattleUIArgs args) {
        OnAllyHPChanged(args.allyHPFillAmounts);
        OnEnemyHPChanged(args.enemyHPFillAmounts);
        OnCostChanged(args.currentCost, args.maxCost);
    }

    private void OnAllyHPChanged(float[] args) {
        for (int i = 0; i < _allyHPBarImage.Length; ++i) {
            if (args == null || args.Length <= i) {
                SetImageFillAmount(_allyHPBarImage[i], 1f);
            }
            else {
                SetImageFillAmount(_allyHPBarImage[i], args[i]);
            }   
        }
    }

    private void OnEnemyHPChanged(float[] args) {
        for (int i = 0; i < _enemyHPBarImage.Length; ++i) {
            if (args == null || args.Length <= i) {
                SetImageFillAmount( _enemyHPBarImage[i], 1f);
            }
            else {
                SetImageFillAmount( _enemyHPBarImage[i], args[i]);
            }   
        }
    }

    private void OnCostChanged(int currentCost, int maxCost) {
        _costText.text = currentCost.ToString() + SlashString + maxCost.ToString();
    }

    private void SetImageFillAmount(Image image, float amount) {
        image.fillAmount = amount;
    }
}
