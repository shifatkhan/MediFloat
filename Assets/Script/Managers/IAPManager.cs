using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

/// <summary>
/// Deriving the purchaser class from <see cref="IStoreListener"/> enables it
/// to receive messagges from Unity Purchasing.
/// </summary>
public class IAPManager : Singleton<IAPManager>, IStoreListener
{
    private static IStoreController _storeController;           // Unity purchasing system.
    private static IExtensionProvider _storeExtensionProvider;  // Store-specific purchasing subsystems.

    // PRODUCTS.
    public static string CUSTOM_BREATHING = "custombreathing";

    private bool _customBreathingUnlocked = false;
    private bool CustomBreathingUnlocked => _customBreathingUnlocked;

    private void Start()
    {
        // If we didn't set up the Unity Purchasing reference.
        if(_storeController == null)
        {
            // Begin to configure our connection to Purchasing.
            InitializePurchasing();
        }

        // Validate product owned.
        CheckProduct(CUSTOM_BREATHING);
    }

    public void InitializePurchasing()
    {

    }

    /// <summary>
    /// Validates if user owns product. Needed in case of refund.
    /// </summary>
    /// <param name="productID">Product to validate.</param>
    public void CheckProduct(string productID)
    {
#if UNITY_EDITOR
        // Do nothing.
#elif UNITY_ANDROD
        StartCoroutine(CheckProductCo(productID));
#endif
    }

    private IEnumerator CheckProductCo(string productID)
    {
        yield return new WaitForSeconds(5);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        throw new System.NotImplementedException();
    }
}
