using UnityEngine;
using UnityEngine.UI;

public class ControlsSchemeSettings : MonoBehaviour
{
    public delegate void OnKeyboardControlChangedHandler(bool scheme);
    public event OnKeyboardControlChangedHandler OnKeyboardControlChanged;
    public delegate void OnGamepadControlChangedHandler(bool scheme);
    public event OnGamepadControlChangedHandler OnGamepadControlChanged;

    private Switch _keyboardSwitch;
    private Switch _gamepadSwitch;

    private void Start()
    {
        AccountSettings accountSettings = StaticObjects.GetDatabase().GetComponent<AccountSettings>();
        accountSettings.OnKeyboardControlSchemeReloaded += ReloadKeyboardControlScheme;
        accountSettings.OnGamepadControlSchemeReloaded += ReloadGamepadControlScheme;

        Switch[] switches = GetComponentsInChildren<Switch>();
        _keyboardSwitch = switches[0];
        _gamepadSwitch = switches[1];
    }

    public void ChangeKeyboardControl(bool scheme)
    {
        OnKeyboardControlChanged(scheme);
    }

    public void ChangeGamepadControl(bool scheme)
    {
        OnGamepadControlChanged(scheme);
    }

    private void ReloadKeyboardControlScheme(bool scheme)
    {
        _keyboardSwitch.isOn = scheme;
    }

    private void ReloadGamepadControlScheme(bool scheme)
    {
        _gamepadSwitch.isOn = scheme;
    }
}
