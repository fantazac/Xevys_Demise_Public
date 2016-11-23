using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class ControlsSchemeSettings : MonoBehaviour
{
    public delegate void OnKeyboardControlChangedHandler(bool control);
    public event OnKeyboardControlChangedHandler OnKeyboardControlChanged;

    public delegate void OnGamepadControlChangedHandler(bool control);
    public event OnGamepadControlChangedHandler OnGamepadControlChanged;

    public void ChangeKeyboardControl(bool control)
    {
        OnKeyboardControlChanged(control);
    }

    public void ChangeGamepadControl(bool control)
    {
        OnGamepadControlChanged(control);
    }
}
