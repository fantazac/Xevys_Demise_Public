using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class StaticObjects : MonoBehaviour
{
    private static GameObject _player;
    private static GameObject _panelUI;
    private static GameObject _pauseMenuPanel;
    private static GameObject _healthBar;
    private static GameObject _itemCanvas;
    private static GameObject _database;
    private static PlayerState _playerState;

    private void Start()
    {
        _player = GameObject.Find("Character");
        _panelUI = GameObject.Find("PanelUI");
        _pauseMenuPanel = GameObject.Find("PauseMenuPanel");
        _healthBar = GameObject.Find("HealthBar");
        _itemCanvas = GameObject.Find("ItemCanvas");
        _database = GameObject.Find("Database");
        _playerState = _player.GetComponent<PlayerState>();
    }

    public static GameObject GetPlayer()
    {
        return _player;
    }

    public static PlayerState GetPlayerState()
    {
        return _playerState;
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

    public static GameObject GetDatabase()
    {
        return _database;
    }
}
