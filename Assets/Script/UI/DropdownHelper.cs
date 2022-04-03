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
    }

    public BreathingConfig GetSelectedItem()
    {
        return _items[_dropdown.value];
    }
}
