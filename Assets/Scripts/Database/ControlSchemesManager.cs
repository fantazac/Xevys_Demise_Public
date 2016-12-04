using System;
using UnityEngine;
using UnityEngine.UI;

public class ControlSchemesManager : MonoBehaviour
{
    public delegate void OnKeyboardControlChangedHandler(int scheme);
    public event OnKeyboardControlChangedHandler OnKeyboardControlChanged;
    public delegate void OnGamepadControlChangedHandler(int scheme);
    public event OnGamepadControlChangedHandler OnGamepadControlChanged;

    private Switch _keyboardSwitch;
    private Switch _gamepadSwitch;

    private void Start()
    {
        AccountSettingsDataHandler accountSettingsDataHandler = StaticObjects.GetDatabase().GetComponent<AccountSettingsDataHandler>();
        accountSettingsDataHandler.OnKeyboardControlSchemeReloaded += ReloadKeyboardControlScheme;
        accountSettingsDataHandler.OnGamepadControlSchemeReloaded += ReloadGamepadControlScheme;

        Switch[] switches = GetComponentsInChildren<Switch>();
        _keyboardSwitch = switches[0];
        _gamepadSwitch = switches[1];
    }

    public void ChangeKeyboardControl(bool scheme)
    {
        OnKeyboardControlChanged(Convert.ToInt32(scheme));
    }

    public void ChangeGamepadControl(bool scheme)
    {
        OnGamepadControlChanged(Convert.ToInt32(scheme));
    }

    private void ReloadKeyboardControlScheme(int scheme)
    {
        _keyboardSwitch.isOn = Convert.ToBoolean(scheme);
    }

    private void ReloadGamepadControlScheme(int scheme)
    {
        _gamepadSwitch.isOn = Convert.ToBoolean(scheme);
    }
}
