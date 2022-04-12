using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private DropdownHelper _dropdown;

    [SerializeField]
    private CanvasGroupHelper _customFields;

    public void OnDropdownChange(int dropdownIndex = -1)
    {
        if (_dropdown.GetSelectedValue().ToLower().Contains("custom"))
        {
            _customFields.gameObject.SetActive(true);
            _customFields.Show();
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
