using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace OutGame
{
    public class CharacterStatUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI attackPowerText;
        [SerializeField] private TextMeshProUGUI defencePowerText;
        [SerializeField] private TextMeshProUGUI criticalText;
        [SerializeField] private TextMeshProUGUI criticalDamangeText;

        private Data.CharacterStat targetStat;

        public void OnEnable()
        {
            // SetUI(FBControl.FirebaseManager.Instance.UserData.CurrentCharacter);   
        }

        public void SetUI( int characterIndex )
        {
            targetStat = Data.CharacterStatDataParser.Instance.GetCharacterStat(characterIndex);

            attackPowerText.text = targetStat.AttackPower.ToString();
            defencePowerText.text = targetStat.DefencePower.ToString();
            criticalText.text = targetStat.Critical.ToString();
            criticalDamangeText.text = targetStat.CriticalDamage.ToString();
        }
    }
}