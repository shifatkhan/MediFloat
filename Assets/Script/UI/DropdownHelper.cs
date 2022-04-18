using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

public class DropdownHelper : MonoBehaviour
{
    [SerializeField]
    private List<BreathingConfig> _items;

    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        RefreshList();
    }

    public void RefreshList()
    {
        _dropdown.ClearOptions();
        foreach (var breathingConfig in _items)
        {
            _dropdown.options.Add(new OptionData(breathingConfig.Name));
        }

        if (_dropdown.options.Count > 0)
        {
            // TODO: Dropdown doesn't show the selected index 0, so I select 1 first. Fix it.
            _dropdown.value = 1;
            _dropdown.value = 0;
        }
    }

    public BreathingConfig GetSelectedItem()
    {
        return _items[_dropdown.value];
    }

    public string GetSelectedValue()
    {
        return _dropdown.options[_dropdown.value].text;
    }

    public void SetSelected(int index)
    {
        if (index < 0 || index > _dropdown.options.Count - 1)
            return;

        _dropdown.value = index;
    }
}
