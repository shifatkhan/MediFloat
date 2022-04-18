using System;
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

    private void Start()
    {
        // If we didn't set up the Unity Purchasing reference ...
        if(_storeController == null)
        {
            // Begin to configure our connection to Purchasing.
            InitializePurchasing();
        }
    }
    
    public bool IsInitialized()
    {
        return _storeController != null && _storeExtensionProvider != null;
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell/restore via its ID and link it to store-specific ID.
        //builder.AddProduct(EXAMPLE_CONSUMABLE, ProductType.Consumable);
        builder.AddProduct(IAPItems.CUSTOM_BREATHING.ToString(), ProductType.NonConsumable);

        // Kick off the remainder of the set-up with an asynchronous call, passing the config
        // and this class' instance. Expect a response either in OnInitialized or OnInitializedFailed.
        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyCustomBreathing()
    {
        BuyProductID(IAPItems.CUSTOM_BREATHING.ToString());
    }

    public string GetProductPriceFromStore(string id)
    {
        if (_storeController != null && _storeController.products != null)
        {
            return _storeController.products.WithID(id).metadata.localizedPriceString;
        }
        else
        {
            return "";
        }
    }

    private void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            // Find product ref with general product ID and Purchasing system's products list.
            Product product = _storeController.products.WithID(productId);

            // If product was found for this device's store and product is ready to be sold ...
            if (product != null && product.availableToPurchase)
            {
                DebugHelper.Log(this.GetType().Name, $"BuyProductID > Purchasing product asynchronously: '{product.definition.id}'");

                // Buy the product. Expect a response through ProcessPurchase or OnPurchaseFailed (async)
                _storeController.InitiatePurchase(product);
            }
            else
            {
                // Product look-up failed..
                DebugHelper.Log(this.GetType().Name, $"BuyProductID > Purchasing product FAILED: product not found or not for sale.");
            }
        }
        else
        {
            // Purchasing not initialized yet. Consider waiting longer or retrying initialization.
            DebugHelper.Log(this.GetType().Name, $"BuyProductID > Purchasing FAILED: Not Initialized.");
        }
    }

    /// <summary>
    /// Restore purchases previously made by the customer. Some platforms automatically restore them (like Google).
    /// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    /// </summary>
    public void RestorePurchases()
    {
        // If purchasing not yet set up ...
        if (!IsInitialized())
        {
            DebugHelper.Log(this.GetType().Name, $"RestorePurchases > Purchasing not initialized.");
            return;
        }

        // If we are on Apple ...
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            DebugHelper.Log(this.GetType().Name, $"RestorePurchases process started...");

            // Fetch Apple store sybsystem.
            var apple = _storeExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the async process of restoring purchases. Expect a confirmation response in
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first of restoration. If no more responses are received on ProcessPurchase then
                // no purchases are available to be restored.
                DebugHelper.Log(this.GetType().Name, $"RestorePurchases continuing: {result}. If no furthur messages, no purchases available to restore.");
            });
        }
        else
        {
            // We are not running on Apple. No work necessary to restore purchases.
            DebugHelper.Log(this.GetType().Name, $"RestorePurchases not needed on this device (since it's not Apple).");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        DebugHelper.Log(this.GetType().Name, $"OnInitialized: PASS");

        // Purchasing system.
        _storeController = controller;
        // Store-specific subsystem.
        _storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        DebugHelper.Log(this.GetType().Name, $"OnInitializeFAILED: {error.ToString()}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        DebugHelper.Log(this.GetType().Name, $"OnPurchaseFAILED for product {product.definition.id}, {failureReason.ToString()}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.CUSTOM_BREATHING.ToString(), StringComparison.Ordinal))
        {
            DebugHelper.Log(this.GetType().Name, $"ProcessPurchase for: {IAPItems.CUSTOM_BREATHING.ToString()}");

            DataManager.Instance.UnlockPremium();
        }
        //else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, EXAMPLE_PRODUCT, StringComparison.Ordinal))
        //{
        //    DebugHelper.Log(this.GetType().Name, $"ProcessPurchase for: {EXAMPLE_PRODUCT}");
        //    DataManager.Instance.UnluckProduct();
        //}

        return PurchaseProcessingResult.Complete;
    }
}
