using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

[System.Serializable]
public class SOItem
{
    public ProductType ProductType;
    public string ProductID;
}

[System.Serializable]
public class ShopInfo
{
    public ProductType ProductType;
    public string ProductID;
}

public class IAPControl : MonoBehaviour, IStoreListener
{
    public static IAPControl Instance;

    [SerializeField]
    private List<SOItem> packageList;

    private UnityAction<PurchaseEventArgs> actionSuccess = (args) => { };
    private UnityAction actionFail = () => { };

    private string lastBuyProductId;
    private PurchaseEventArgs lastArgs;
    private static IStoreController m_StoreController; // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    // Product identifiers for all products capable of being purchased:
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.

    // Apple App Store-specific product identifier for the subscription product.
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    [SerializeField] private RecipteChecker recipteChecker;

    public void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        foreach (SOItem soProduct in packageList)
            builder.AddProduct(soProduct.ProductID, soProduct.ProductType);

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }

    #region 외부 호출 함수.

    public Product GetProduct(string _productId)
    {
        return m_StoreController.products.WithID(_productId);
    }

    //로딩 추가.
    public bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProduct(ShopInfo _shopInfo, UnityAction<PurchaseEventArgs> _actionSucess, UnityAction _actionFail)
    {
        SOItem item = GetSOItem(_shopInfo.ProductID);

        if (item == null)
        {
            _actionFail?.Invoke();
            return;
        }

        Debug.Log("BuyProduct : " + item.ProductID);

        lastBuyProductId = _shopInfo.ProductID;

        BuyProduct(item.ProductID, _actionSucess, _actionFail);
    }

    public void BuyProduct(string productID, UnityAction<PurchaseEventArgs> _actionSucess, UnityAction _actionFail)
    {
        // Buy the consumable product using its general identifier. Expect a response either
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        actionSuccess = _actionSucess;
        actionFail = _actionFail;
        BuyProductID(productID);
    }

    public SOItem GetSOItem(string _productId)
    {
        SOItem SOItem = packageList.Find(each => { return each.ProductID.Equals(_productId); });

        return SOItem;
    }

    #endregion 외부 호출 함수.

    private void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            //중복 클릭 작업.
            {
            }

            // ... look up the Product reference with the general product identifier and the Purchasing
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ...
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation
                Debug.Log(
                    "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google.
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ...
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result +
                            ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    //
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        OnRecipteCheckEnd(false);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log("Purchase Result args:" + args);

        //recipteChecker.CheckRecipte( args, lastBuyProductId, OnRecipteCheckEnd );

        OnRecipteCheckEnd(true);

        return PurchaseProcessingResult.Complete;
    }

    public void OnRecipteCheckEnd(bool isSuccess)
    {
        if (isSuccess)
        {
            actionSuccess?.Invoke(lastArgs);
        }
        else
        {
            actionFail?.Invoke();
            actionFail = null;
        }
    }

    //결제 실패
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //중복 클릭 제거
        {
        }

        actionFail?.Invoke();
        actionFail = null;
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",
            product.definition.storeSpecificId, failureReason));
    }
}