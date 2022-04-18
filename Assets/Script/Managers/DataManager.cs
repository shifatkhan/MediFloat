using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    private GameObject _premiumIndicator;

    private bool _premiumUnlocked = false;
    public bool PremiumUnlocked => _premiumUnlocked;

    public void UnlockPremium()
    {
        _premiumUnlocked = true;
        _premiumIndicator.SetActive(true);
    }
}
