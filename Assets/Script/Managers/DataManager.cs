using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GenericPopup;

public class DataManager : Singleton<DataManager>
{
    public struct IAPStatus
    {
        public IAPItems item;
        public bool unlocked;
    }

    [Tooltip("List of all IAP and whether they are unlocked or not.")]
    private List<IAPStatus> _IAPStatus = new List<IAPStatus>()
    {
        new IAPStatus(){ item = IAPItems.CUSTOM_BREATHING, unlocked = false },
    };

    [SerializeField]
    private GameObject _premiumIndicator;

    public void UnlockPremium()
    {
        SetIAPStatus(IAPItems.CUSTOM_BREATHING, true);
        _premiumIndicator.SetActive(true);

        var param = new List<GenericPopupButtonParam>();

        GenericPopupButtonParam btnParam = new GenericPopupButtonParam("OK");
        btnParam.OnClick.AddListener(() => GenericPopup.Instance.Hide());

        param.Add(btnParam);

        GenericPopup.Instance.Show("Premium version unlocked!", param);
    }
    
    private void SetIAPStatus(IAPItems item, bool unlocked)
    {
        IAPStatus iap = _IAPStatus.FirstOrDefault(x => x.item == item);
        iap.unlocked = unlocked;
    }

    /// <summary>
    /// Checks whether iap item is unlocked or not.
    /// </summary>
    /// <param name="item">The IAP item to check</param>
    /// <returns>Unlocked state</returns>
    public bool IsIAPUnlocked(IAPItems item)
    {
        IAPStatus iap = _IAPStatus.FirstOrDefault(x => x.item == item);
        return iap.unlocked;
    }
}
