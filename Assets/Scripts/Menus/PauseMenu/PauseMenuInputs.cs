using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenuInputs : MonoBehaviour
{

    public delegate void PauseMenuOntriggerHandler(bool isDead);
    public event PauseMenuOntriggerHandler TriggerAnimations;

    public delegate void OnMainInterfaceIsCurrentHandler(string current);
    public event OnMainInterfaceIsCurrentHandler OnMainInterfaceIsCurrent;

    public delegate void OnOptionsInterfaceIsCurrentHandler(string current);
    public event OnOptionsInterfaceIsCurrentHandler OnOptionsInterfaceIsCurrent;

    public delegate void OnControlsInterfaceIsCurrentHandler(string current);
    public event OnControlsInterfaceIsCurrentHandler OnControlsInterfaceIsCurrent;

    public delegate void OnAudioInterfaceIsCurrentHandler(string current);
    public event OnAudioInterfaceIsCurrentHandler OnAudioInterfaceIsCurrent;

    public delegate void OnMainInterfaceOpendedHandler();
    public event OnMainInterfaceOpendedHandler OnMainInterfaceOpended;

    public delegate void OnReturnToMenuButtonPressedHandler();
    public event OnReturnToMenuButtonPressedHandler OnReturnToMenuButtonPressed;

    private InputManager _inputManager;
    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private EventSystem _pauseMenuEventSystem;

    private bool _canSlide;
    public bool CanSlide { private get { return _canSlide; } set { _canSlide = value; } }

    private GameObject _resumeBtnGameObject;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;
    private WaitForSeconds _waitForOneSecond;

    private void Start()
    {
        _inputManager = StaticObjects.GetPlayer().GetComponentInChildren<InputManager>();
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuCurrentInterfaceAnimator = GameObject.Find(StaticObjects.GetMainObjects().PauseMenuButtons).GetComponent<PauseMenuCurrentInterfaceAnimator>();
        _pauseMenuEventSystem = EventSystem.current;
        _resumeBtnGameObject = GameObject.Find(StaticObjects.GetMainObjects().ResumeBtn);

        _waitForOneSecond = new WaitForSeconds(0.3f);

        _pauseMenuAnimationManager.OnPauseMenuStateChanged += SyncFirstControlOnPauseMenuStateChanged;
        _inputManager.OnPause += PauseMenuTriggered;
        _pauseMenuCurrentInterfaceAnimator.OnBackButtonPressedToClosePauseMenu += PauseMenuTriggered;
        _pauseMenuCurrentInterfaceAnimator.OnPlayerDeathShowDeathInterface += PauseMenuTriggered;
        _pauseMenuCurrentInterfaceAnimator.OnEndShowEndInterface += PauseMenuTriggered;
        _canSlide = true;
    }

    public void PauseMenuTriggered(bool isDead)
    {
        if (CanSlide)
        {
            TriggerAnimations(isDead);
        }
    }

    public void OptionBtnOnClick()
    {
        OnOptionsInterfaceIsCurrent("Options");
    }

    public void QuitBtnOnClick()
    {
        PauseMenuAudioSettingsManager audioSettingsManager = GameObject.Find(StaticObjects.GetMainObjects().PauseMenuAudioOptionsButtons).GetComponent<PauseMenuAudioSettingsManager>();
        audioSettingsManager.SetSoundVolume(audioSettingsManager._sfxVolumeBeforeDesactivate);
        DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<DatabaseController>().SaveAccount();
        OnReturnToMenuButtonPressed();
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

    private void SyncFirstControlOnPauseMenuStateChanged(bool isActive, bool isDead)
    {
        if (isActive)
        {
            OnMainInterfaceIsCurrent("MainOpened");
            OnMainInterfaceOpended();
        }
        else
        {
            StartCoroutine(SlideInterfaceWhenOutOffScreen());
        }

        _pauseMenuEventSystem.SetSelectedGameObject(isActive ? _resumeBtnGameObject : null);
    }

    private IEnumerator SlideInterfaceWhenOutOffScreen()
    {
        yield return _waitForOneSecond;

        OnMainInterfaceIsCurrent("ShowMainInterface");
    }
}
