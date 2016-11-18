﻿using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class StaticObjects : MonoBehaviour
{
    private static GameObject _player;
    private static GameObject _panelUI;
    private static GameObject _pauseMenuPanel;
    private static GameObject _healthBar;
    private static GameObject _itemCanvas;

    private void Start()
    {
        _player = GameObject.Find("Character");
        _panelUI = GameObject.Find("PanelUI");
        _pauseMenuPanel = GameObject.Find("PauseMenuPanel");
        _healthBar = GameObject.Find("HealthBar");
        _itemCanvas = GameObject.Find("ItemCanvas");
    }

    public static GameObject GetPlayer()
    {
        return _player;
    }

    public static GameObject GetPanelUI()
    {
        return _panelUI;
    }

    public static GameObject GetPauseMenuPanel()
    {
        return _pauseMenuPanel;
    }

    public static GameObject GetHealthBar()
    {
        return _healthBar;
    }

    public static GameObject GetItemCanvas()
    {
        return _itemCanvas;
    }
}