using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class StaticObjects : MonoBehaviour
{
    private static GameObject _player;
    private static GameObject _panelUI;
    private static GameObject _pauseMenuPanel;

    private void Start()
    {
        _player = GameObject.Find("Character");
        _panelUI = GameObject.Find("PanelUI");
        _pauseMenuPanel = GameObject.Find("PauseMenuPanel");
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
}
