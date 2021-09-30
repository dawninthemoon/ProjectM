using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using FBControl;

public class IAPBuyButton : MonoBehaviour
{
    [SerializeField] private string productId;
    public void OnClick()
    {
        IAPControl.Instance.BuyProduct( productId, SuccessCallback, FailCallback );
    }

    public void SuccessCallback( PurchaseEventArgs args )
    {
        FirebaseManager.Instance.UserData.UserCurrenyData.FCash += 1000;
    }

    public void FailCallback()
    {
        FirebaseManager.Instance.UserData.UserCurrenyData.FCash += 1000;
    }
}
