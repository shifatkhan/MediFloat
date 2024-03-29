using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A centralized input management system that checks
/// which button is pressed or not.
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
public class InputManager : Singleton<InputManager>
{
    public bool InputEnabled = true;

    public bool MouseButton0 { get; private set; }

    private void Update()
    {
        if (!InputEnabled) return;

        MouseButton0 = Input.GetMouseButton(0);
    }

    public void SetInputEnabled(bool inputEnabled)
    {
        InputEnabled = inputEnabled;
    }
}
