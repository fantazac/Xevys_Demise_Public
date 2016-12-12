using UnityEngine;

public class PauseMenuCurrentInterfaceAnimator : MonoBehaviour
{
    public delegate void OnMainInterfaceFadeHandler();
    public event OnMainInterfaceFadeHandler OnMainInterfaceFade;

    public delegate void OnOptionsInterfaceFadeHandler();
    public event OnOptionsInterfaceFadeHandler OnOptionsInterfaceFade;

    public delegate void OnControlsInterfaceFadeHandler();
    public event OnControlsInterfaceFadeHandler OnControlsInterfaceFade;

    public delegate void OnAudioInterfaceFadeHandler();
    public event OnAudioInterfaceFadeHandler OnAudioInterfaceFade;

    public delegate void OnMainInterfaceIsCurrentHandler(string from);
    public event OnMainInterfaceIsCurrentHandler OnMainInterfaceIsCurrent;

    public delegate void OnOptionsInterfaceIsCurrentHandler(string from);
    public event OnOptionsInterfaceIsCurrentHandler OnOptionsInterfaceIsCurrent;

    public delegate void OnAudioInterfaceIsCurrentHandler(bool isCurrent);
    public event OnAudioInterfaceIsCurrentHandler OnAudioInterfaceIsCurrent;

    public delegate void OnBackButtonPressedToClosePauseMenuHandler(bool isDead);
    public event OnBackButtonPressedToClosePauseMenuHandler OnBackButtonPressedToClosePauseMenu;

    public delegate void OnPlayerDeathShowDeathInterfaceHandler(bool isDead);
    public event OnPlayerDeathShowDeathInterfaceHandler OnPlayerDeathShowDeathInterface;

    public delegate void OnEndShowEndInterfaceHandler(bool isDead);
    public event OnEndShowEndInterfaceHandler OnEndShowEndInterface;

    [SerializeField] 
    private PickUpArtefact _voidArtefact;

    private PauseMenuInputs _pauseMenuInputs;
    private InputManager _inputManager;
    private Animator _animator;
    private Health _playeHealth;
    private ShowCreditsScene _credits;
    private PauseMenuGroupButtonsFadeListener _pauseMenuGroupButtonsFadeListener;

    private string _currentInterface;

    private void Start()
    {
        _pauseMenuInputs = StaticObjects.GetPauseMenuPanel().GetComponentInChildren<PauseMenuInputs>();
        _inputManager = StaticObjects.GetPlayer().GetComponentInChildren<InputManager>();
        _playeHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _pauseMenuGroupButtonsFadeListener = GameObject.Find(StaticObjects.GetMainObjects().PauseMenuAudioOptionsButtons).GetComponent<PauseMenuGroupButtonsFadeListener>();
        _credits = StaticObjects.GetPauseMenuPanel().GetComponentInChildren<ShowCreditsScene>();

        _pauseMenuInputs.OnOptionsInterfaceIsCurrent += OptionsInterfaceIsCurrent;
        _pauseMenuInputs.OnMainInterfaceIsCurrent += MainInterfaceIsCurrent;
        _pauseMenuInputs.OnControlsInterfaceIsCurrent += ControlsInterfaceIsCurrent;
        _pauseMenuInputs.OnAudioInterfaceIsCurrent += AudioInterfaceIsCurrent;
        _inputManager.OnBackButtonPressedInMenu += OnGamepadBackBtnPressed;
        _playeHealth.OnDeath += OnPlayerDeath;
        _pauseMenuGroupButtonsFadeListener.OnAudioInterfaceFadingEnded += AudioInterfaceFadingEnded;
        _voidArtefact.OnGameEnded += OnPlayerFinishedTheGame;

        _animator = GetComponent<Animator>();

        _currentInterface = "Main";
    }

    private void OnPlayerFinishedTheGame()
    {
        _credits.ShowCredits();
    }

    private void OnPlayerDeath()
    {
        _animator.SetTrigger("ShowDeathInterface");
        OnPlayerDeathShowDeathInterface(true);
    }

    private void OnGamepadBackBtnPressed()
    {
        switch (_currentInterface)
        {
            case "Main":
                OnBackButtonPressedToClosePauseMenu(false);
                break;
            case "Options":
                MainInterfaceIsCurrent("Main");
                break;
            case "Controls":
                OptionsInterfaceIsCurrent("Options");
                break;
            case "Audio":
                OptionsInterfaceIsCurrent("Options");
                break;
        }
    }

    private void OptionsInterfaceIsCurrent(string target)
    {
        if (target != _currentInterface && _currentInterface == "Main")
        {
            _animator.SetTrigger("OptionsSlideIn");
            _currentInterface = target;
            OnMainInterfaceFade();
        }
        else if (target != _currentInterface && _currentInterface == "Controls")
        {
            _animator.SetTrigger("OptionsSlideIn");
            _currentInterface = target;
            OnControlsInterfaceFade();
            OnOptionsInterfaceIsCurrent("Controls");
        }
        else if (target != _currentInterface && _currentInterface == "Audio")
        {
            _animator.SetTrigger("OptionsSlideIn");
            _currentInterface = target;
            OnAudioInterfaceFade();
            OnOptionsInterfaceIsCurrent("Audio");
        }
    }

    private void MainInterfaceIsCurrent(string target)
    {
        if (target != _currentInterface)
        {
            if (target == "ShowMainInterface")
            {
                _animator.SetTrigger(target);
                _currentInterface = target;
            }
            else if (target == "Main")
            {
                _animator.SetTrigger("MainSlideIn");
                _currentInterface = target;
                OnOptionsInterfaceFade();
                OnMainInterfaceIsCurrent("Main");
            }
            else
            {
                _currentInterface = "Main";
            }
        }
    }

    private void ControlsInterfaceIsCurrent(string target)
    {
        if (target != _currentInterface && _currentInterface == "Options")
        {
            _animator.SetTrigger("ControlsSlideIn");
            _currentInterface = target;
            OnOptionsInterfaceFade();
        }
    }

    private void AudioInterfaceIsCurrent(string target)
    {
        if (target != _currentInterface && _currentInterface == "Options")
        {
            _animator.SetTrigger("AudioSlideIn");
            _currentInterface = target;
            OnOptionsInterfaceFade();
            OnAudioInterfaceIsCurrent(true);
        }
    }

    private void AudioInterfaceFadingEnded()
    {
        OnAudioInterfaceIsCurrent(false);
    }
}