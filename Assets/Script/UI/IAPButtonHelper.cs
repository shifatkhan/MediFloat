using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// A custom IAP Button component to load item price.
/// </summary>
public class IAPButtonHelper : MonoBehaviour
{
    [SerializeField]
    private IAPItems _itemType;
    public IAPItems ItemType => _itemType;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private string _defaultText = "Buy";

    private void Start()
    {
        _text.text = _defaultText;
        StartCoroutine(LoadPriceCo());
    }

    public void OnBuy()
    {
        switch (_itemType)
        {
            case IAPItems.CUSTOM_BREATHING:
                IAPManager.Instance.BuyCustomBreathing();
                break;
            default:
                DebugHelper.LogError(this.GetType().Name, $"OnBuy Incorrect item type: {_itemType.ToString()}");
                break;
        }
    }

    private IEnumerator LoadPriceCo()
    {
        while (!IAPManager.Instance.IsInitialized())
            yield return null;

        string loadedPrice = "";

        switch (_itemType)
        {
            case IAPItems.CUSTOM_BREATHING:
                loadedPrice = IAPManager.Instance.GetProductPriceFromStore(_itemType.ToString());
                break;
            default:
                DebugHelper.LogError(this.GetType().Name, $"LoadPriceCo Incorrect item type: {_itemType.ToString()}");
                break;
        }

        _text.text = _defaultText + " " + loadedPrice;
    }
}
