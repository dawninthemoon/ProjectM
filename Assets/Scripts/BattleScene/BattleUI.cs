using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour {
    [SerializeField] Image[] _allyHPBarImage = null;
    [SerializeField] Image[] _enemyHPBarImage = null;

    public void OnAllyHPChanged(float[] fillAmounts) {
        for (int i = 0; i < _allyHPBarImage.Length; ++i) {
            if (fillAmounts == null || fillAmounts.Length <= i) {
                SetImageFillAmount(_allyHPBarImage[i], 1f);
            }
            else {
                SetImageFillAmount(_allyHPBarImage[i], fillAmounts[i]);
            }   
        }
    }

    public void OnEnemyHPChanged(float[] fillAmounts) {
        for (int i = 0; i < _enemyHPBarImage.Length; ++i) {
            if (fillAmounts == null || fillAmounts.Length <= i) {
                SetImageFillAmount( _enemyHPBarImage[i], 1f);
            }
            else {
                SetImageFillAmount( _enemyHPBarImage[i], fillAmounts[i]);
            }   
        }
    }

    private void SetImageFillAmount(Image image, float amount) {
        image.fillAmount = amount;
    }
}
