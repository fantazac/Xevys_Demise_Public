using UnityEngine;

public class MainMenuCurrentInterfaceAnimator : MonoBehaviour
{
    public delegate void OnAccountInterfaceFadeHandler();
    public event OnAccountInterfaceFadeHandler OnAccountInterfaceFade;

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

    public delegate void OnAccountsInterfaceIsCurrentHandler(string from);
    public event OnAccountsInterfaceIsCurrentHandler OnAccountsInterfaceIsCurrent;

    public delegate void OnOptionsInterfaceQuitHandler();
    public event OnOptionsInterfaceQuitHandler OnOptionsInterfaceQuit;

    private MainMenuInputs _mainMenuInputs;
    private Animator _animator;
    private string _currentInterface;

    private void Start()
    {
        _mainMenuInputs = MainMenuStaticObjects.GetMainMenuPanel().GetComponentInChildren<MainMenuInputs>();

        _mainMenuInputs.OnOptionsInterfaceIsCurrent += OptionsInterfaceIsCurrent;
        _mainMenuInputs.OnMainInterfaceIsCurrent += MainInterfaceIsCurrent;
        _mainMenuInputs.OnControlsInterfaceIsCurrent += ControlsInterfaceIsCurrent;
        _mainMenuInputs.OnAudioInterfaceIsCurrent += AudioInterfaceIsCurrent;
        _mainMenuInputs.OnAccountInterfaceIsCurrent += AccountInterfaceIsCurrent;
        _mainMenuInputs.OnGamepadBackButtonPressed += OnGamepadBackBtnPressed;

        _animator = GetComponent<Animator>();

        _currentInterface = "Account";
    }

    private void OnGamepadBackBtnPressed()
    {
        switch (_currentInterface)
        {
            case "Main":
                AccountInterfaceIsCurrent("Account");
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

    private void AccountInterfaceIsCurrent(string target)
    {
        if (target != _currentInterface)
        {
            _animator.SetTrigger("AccountSlideIn");
            _currentInterface = target;
            OnMainInterfaceFade();
            OnAccountsInterfaceIsCurrent("");
        }
    }

    private void MainInterfaceIsCurrent(string target)
    {
        if (target != _currentInterface && _currentInterface == "Options")
        {
            _animator.SetTrigger("MainSlideIn");
            _currentInterface = target;
            OnOptionsInterfaceFade();
            OnMainInterfaceIsCurrent("Options");
            OnOptionsInterfaceQuit();
        }
        else if(target != _currentInterface && _currentInterface == "Account")
        {
            _animator.SetTrigger("MainSlideIn");
            _currentInterface = target;
            OnAccountInterfaceFade();
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
        }
    }
}