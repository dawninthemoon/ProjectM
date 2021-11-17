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
    [SerializeField] Image[] _allyHPUIImages = null;
    [SerializeField] Image[] _allyEXPUIImages = null;
    [SerializeField] Image[] _enemyHPUIImages = null;
    [SerializeField] TextMeshProUGUI _costText = null;
    private static readonly string SlashString = "/";

    public void OnSkillUsed(BattleUIArgs args) {
        foreach (var ui in _allyHPUIImages) {
            ui.transform.parent.gameObject.SetActive(true);
        }
        foreach (var ui in _enemyHPUIImages) {
            ui.transform.parent.gameObject.SetActive(true);
        }

        OnAllyHPChanged(args.allyHPFillAmounts);
        OnEnemyHPChanged(args.enemyHPFillAmounts);
        OnCostChanged(args.currentCost, args.maxCost);
    }

    private void OnAllyHPChanged(float[] args) {
        for (int i = 0; i < _allyHPUIImages.Length; ++i) {
            if (args == null || args.Length <= i) {
                SetImageFillAmount(_allyHPUIImages[i], 1f);
                SetImageFillAmount(_allyHPUIImages[i], 1f);
            }
            else {
                SetImageFillAmount(_allyHPUIImages[i], args[i]);
                SetImageFillAmount(_allyHPUIImages[i], args[i]);
            }   
        }
    }

    private void OnEnemyHPChanged(float[] args) {
        for (int i = 0; i < _enemyHPUIImages.Length; ++i) {
            if (args == null || args.Length <= i) {
                SetImageFillAmount(_enemyHPUIImages[i], 1f);
            }
            else {
                SetImageFillAmount(_enemyHPUIImages[i], args[i]);
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
