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
    private static GameObject _tags;
    private static GameObject _mainCamera;
    private static PlayerState _playerState;
    private static MainGameObjects _mainObjects;
    private static GameObjectTags _objectTags;
    private static AnimationTags _animationTags;
    private static GameObject _respawnEnemy;
    private static GameObject _cinematic;
    private static GameObject _pause;               

    public static int AccountId { get; set; }

    private void Start()
    {
        //Je dois utiliser le string "Tags" ici car _findTags n'a pas encore de valeur
        _tags = GameObject.Find("Tags");

        _mainObjects = _tags.GetComponent<MainGameObjects>();
        _objectTags = _tags.GetComponent<GameObjectTags>();
        _animationTags = _tags.GetComponent<AnimationTags>();

        _player = GameObject.Find(_mainObjects.Character);
        _panelUI = GameObject.Find(_mainObjects.PanelUI);
        _pauseMenuPanel = GameObject.Find(_mainObjects.PauseMenuPanel);
        _healthBar = GameObject.Find(_mainObjects.HealthBar);
        _itemCanvas = GameObject.Find(_mainObjects.ItemCanvas);
        _mainCamera = GameObject.Find(_mainObjects.MainCamera);
        _respawnEnemy = GameObject.Find(_mainObjects.RespawnEnemy);
        _cinematic = GameObject.Find(_mainObjects.Cinematic);
        _pause = GameObject.Find(_mainObjects.Pause);

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

    public static MainGameObjects GetMainObjects()
    {
        return _mainObjects;
    }

    public static GameObjectTags GetObjectTags()
    {
        return _objectTags;
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

    public static GameObject GetCinematic()
    {
        return _cinematic;
    }

    public static GameObject GetPause()
    {
        return _pause;
    }

    private void OnDestroy()
    {
        _player = null;
        _panelUI = null;
        _pauseMenuPanel = null;
        _healthBar = null;
        _itemCanvas = null;
        _tags = null;
        _mainCamera = null;
        _playerState = null;
        _mainObjects = null;
        _objectTags = null;
        _animationTags = null;
        _respawnEnemy = null;
        _cinematic = null;
        _pause = null;
    }
}
