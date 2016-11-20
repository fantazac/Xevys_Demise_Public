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

    public delegate void OnMainInterfaceIsCurrentHandler(string current);
    public event OnMainInterfaceIsCurrentHandler OnMainInterfaceIsCurrent;

    public delegate void OnOptionsInterfaceIsCurrentHandler(string current);
    public event OnOptionsInterfaceIsCurrentHandler OnOptionsInterfaceIsCurrent;

    public delegate void OnControlsInterfaceIsCurrentHandler(string current);
    public event OnControlsInterfaceIsCurrentHandler OnControlsInterfaceIsCurrent;

    public delegate void OnAudioInterfaceIsCurrentHandler(string current);
    public event OnAudioInterfaceIsCurrentHandler OnAudioInterfaceIsCurrent;

    private bool _canSlide;
    public bool CanSlide { private get { return _canSlide; } set { _canSlide = value; } }

    private GameObject _resumeBtnGameObject;

    private WaitForSeconds _waitForOneSecond;

    private void Start()
    {
        _inputManager = StaticObjects.GetPlayer().GetComponentInChildren<InputManager>();
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuEventSystem = EventSystem.current;
        _resumeBtnGameObject = GameObject.Find("ResumeBtn");

        _waitForOneSecond = new WaitForSeconds(0.3f);

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
        OnOptionsInterfaceIsCurrent("Options");
    }

    public void QuitBtnOnClick()
    {
        Application.Quit();
    }

    public void ControlsBtnOnClick()
    {
        OnControlsInterfaceIsCurrent("Controls");
    }

    public void AudioBtnOnClick()
    {
        OnAudioInterfaceIsCurrent("Audio");
    }

    public void OptionsBackBtnOnClick()
    {
        OnMainInterfaceIsCurrent("Main");
    }

    public void ControlsBackBtnOnClick()
    {
        OnOptionsInterfaceIsCurrent("Options");
    }

    public void AudioBackBtnOnClick()
    {
        OnOptionsInterfaceIsCurrent("Options");
    }

    private void SyncFirstControlOnPauseMenuStateChanged(bool isActive)
    {
        if (isActive)
        {
            OnMainInterfaceIsCurrent("Main");
        }
        else
        {
            StartCoroutine("SlideInterfaceWhenOutOffScreen");
        }

        _pauseMenuEventSystem.SetSelectedGameObject(isActive ? _resumeBtnGameObject : null);
    }

    private IEnumerator SlideInterfaceWhenOutOffScreen()
    {
        yield return _waitForOneSecond;

        OnMainInterfaceIsCurrent("ShowMainInterface");
    }
}
