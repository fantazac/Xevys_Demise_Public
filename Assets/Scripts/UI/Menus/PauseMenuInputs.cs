using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenuInputs : MonoBehaviour
{

    public delegate void PauseMenuOntriggerHandler();
    public event PauseMenuOntriggerHandler TriggerAnimations;

    private InputManager _inputManager;
    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private EventSystem _pauseMenuEventSystem;

    private bool _canSlide;
    public bool CanSlide { private get { return _canSlide; } set { _canSlide = value; } }

    private void Start()
    {
        _inputManager = StaticObjects.GetPlayer().GetComponentInChildren<InputManager>();
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuEventSystem = EventSystem.current;

        _pauseMenuAnimationManager.OnPauseMenuStateChanged += SyncFirstControlOnPauseMenuStateChanged;
        _inputManager.OnPause += PauseMenuTriggered;
        _canSlide = true;
    }

    public void PauseMenuTriggered()
    {
        if (CanSlide)
        {
            TriggerAnimations();
        }
    }

    public void OptionBtnOnClick()
    {
        _pauseMenuEventSystem.SetSelectedGameObject(transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject);
    }

    public void QuitBtnOnClick()
    {
        Application.Quit();
    }

    public void ControlsBtnOnClick()
    {
        _pauseMenuEventSystem.SetSelectedGameObject(transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject);
    }

    public void AudioBtnOnClick()
    {
        _pauseMenuEventSystem.SetSelectedGameObject(transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject);
    }



    public void OptionsBackBtnOnClick()
    {
        _pauseMenuEventSystem.SetSelectedGameObject(transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject);
    }

    public void ControlsBackBtnOnClick()
    {
        _pauseMenuEventSystem.SetSelectedGameObject(transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject);
    }

    public void AudioBackBtnOnClick()
    {
        _pauseMenuEventSystem.SetSelectedGameObject(transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject);
    }

    private void SyncFirstControlOnPauseMenuStateChanged(bool isActive)
    {
        _pauseMenuEventSystem.SetSelectedGameObject(isActive ? transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject : null);
    }
}
