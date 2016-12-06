using System;
using UnityEngine;
using UnityEngine.UI;

public class ControlSchemesManager : MonoBehaviour
{
    public delegate void OnKeyboardControlChangedHandler(int scheme);
    public static event OnKeyboardControlChangedHandler OnKeyboardControlChanged;
    public delegate void OnGamepadControlChangedHandler(int scheme);
    public static event OnGamepadControlChangedHandler OnGamepadControlChanged;
    public delegate void OnNewMenuStartedHandler();
    public static event OnNewMenuStartedHandler OnNewMenuStarted;

    private Switch _keyboardSwitch;
    private Switch _gamepadSwitch;

    private void Start()
    {
        AccountSettingsDataHandler accountSettingsDataHandler = DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountSettingsDataHandler>();
        accountSettingsDataHandler.OnKeyboardControlSchemeReloaded += ReloadKeyboardControlScheme;
        accountSettingsDataHandler.OnGamepadControlSchemeReloaded += ReloadGamepadControlScheme;

        Switch[] switches = GetComponentsInChildren<Switch>();
        _keyboardSwitch = switches[0];
        _gamepadSwitch = switches[1];

        if (OnNewMenuStarted != null)
        {
            OnNewMenuStarted();
        }
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
