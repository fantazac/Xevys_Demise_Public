using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class StaticObjects : MonoBehaviour
{
    private static GameObject _player;
    private static GameObject _panelUI;
    private static GameObject _pauseMenu;
    private static GameObject _healthBar;

    private void Start()
    {
        _player = GameObject.Find("Character");
        _panelUI = GameObject.Find("PanelUI");
        _pauseMenu = GameObject.Find("PauseMenu");
        _healthBar = GameObject.Find("HealthBar");
    }

    public static GameObject GetPlayer()
    {
        return _player;
    }

    public static GameObject GetPanelUI()
    {
        return _panelUI;
    }

    public static GameObject GetPauseMenu()
    {
        return _pauseMenu;
    }

    public static GameObject GetHealthBar()
    {
        return _healthBar;
    }
}
