using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirebaseManager = FBControl.FirebaseManager;

public class CurrencyText : TweenText
{
    [SerializeField] private CurrencyType currencyType;
    [SerializeField] private bool isTween = true;

    private int beforeCurrency = 0;

    public void Start()
    {
        FirebaseManager.Instance.UserData.UserCurrenyData.OnChangeCurrenyEvent += SetCurrencyText;
    }

    public void OnDestroy()
    {
        FirebaseManager.Instance.UserData.UserCurrenyData.OnChangeCurrenyEvent -= SetCurrencyText;
    }

    public void SetCurrencyText( CurrencyType currencyType )
    {
        if( this.currencyType != currencyType )
            return;

        if( isTween )
        {
            switch( currencyType )
            {
                case CurrencyType.Gold:
                    base.SetNum( beforeCurrency, FirebaseManager.Instance.UserData.UserCurrenyData.Gold );
                    beforeCurrency = FirebaseManager.Instance.UserData.UserCurrenyData.Gold;
                break;
                case CurrencyType.FreeCash:
                    base.SetNum( beforeCurrency, FirebaseManager.Instance.UserData.UserCurrenyData.FCash ); 
                    beforeCurrency = FirebaseManager.Instance.UserData.UserCurrenyData.FCash;
                break;
                case CurrencyType.PaidCash:
                    base.SetNum( beforeCurrency, FirebaseManager.Instance.UserData.UserCurrenyData.PCash );
                    beforeCurrency = FirebaseManager.Instance.UserData.UserCurrenyData.PCash;
                break;
            }  
        }
        else
        {
            switch( currencyType )
            {
                case CurrencyType.Gold:
                    beforeCurrency = FirebaseManager.Instance.UserData.UserCurrenyData.Gold;
                    base.text.text = beforeCurrency.ToString();
                break;
                case CurrencyType.FreeCash: 
                    beforeCurrency = FirebaseManager.Instance.UserData.UserCurrenyData.FCash;
                    base.text.text = beforeCurrency.ToString();
                break;
                case CurrencyType.PaidCash:
                    beforeCurrency = FirebaseManager.Instance.UserData.UserCurrenyData.PCash;
                    base.text.text = beforeCurrency.ToString();
                break;
            }
        }
    }
}
