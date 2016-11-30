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
    private static GameObject _tags;
    private static GameObject _mainCamera;
    private static PlayerState _playerState;
    private static FindTags _findTags;
    private static UnityTags _unityTags;
    private static AnimationTags _animationTags;
    private static GameObject _respawnEnemy;

    private void Start()
    {
        //Je dois utiliser le string "Tags" ici car _findTags n'a pas encore de valeur
        _tags = GameObject.Find("Tags");

        _findTags = _tags.GetComponent<FindTags>();
        _unityTags = _tags.GetComponent<UnityTags>();
        _animationTags = _tags.GetComponent<AnimationTags>();

        _player = GameObject.Find(_findTags.Character);
        _panelUI = GameObject.Find(_findTags.PanelUI);
        _pauseMenuPanel = GameObject.Find(_findTags.PauseMenuPanel);
        _healthBar = GameObject.Find(_findTags.HealthBar);
        _itemCanvas = GameObject.Find(_findTags.ItemCanvas);
        _database = GameObject.Find(_findTags.Database);
        _mainCamera = GameObject.Find(_findTags.MainCamera);
        _respawnEnemy = GameObject.Find(_findTags.RespawnEnemy);
        
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

    public static FindTags GetFindTags()
    {
        return _findTags;
    }

    public static UnityTags GetUnityTags()
    {
        return _unityTags;
    }

    public static AnimationTags GetAnimationTags()
    {
        return _animationTags;
    }

    public static GameObject GetRespawnEnemy()
    {
        return _respawnEnemy;
    }

    public static GameObject GetMainCamera()
    {
        return _mainCamera;
    }
}
