using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private DropdownHelper _dropdown;

    [SerializeField]
    private CanvasGroupHelper _customFields;

    [SerializeField]
    private CanvasGroupHelper _IAPPopup;

    public void OnDropdownChange(int dropdownIndex = -1)
    {
        if (_dropdown.GetSelectedValue().ToLower().Contains("custom"))
        {
            if (DataManager.Instance.PremiumUnlocked)
            {
                _customFields.gameObject.SetActive(true);
                _customFields.Show();
            }
            else
            {
                _dropdown.SetSelected(0);
                _IAPPopup.gameObject.SetActive(true);
                _IAPPopup.Show();
            }
        }
        else
        {
            _customFields.Hide(() => _customFields.gameObject.SetActive(true));
        }
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
