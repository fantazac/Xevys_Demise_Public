using UnityEngine;
using System.Collections;

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

    private PauseMenuInputs _pauseMenuInputs;
    private Animator _animator;
    private PauseMenuAnimationManager _pauseMenuAnimationManager;

    private string _currentInterface;

    private void Start()
    {
        _pauseMenuInputs = StaticObjects.GetPauseMenuPanel().GetComponentInChildren<PauseMenuInputs>();
        _pauseMenuInputs.OnOptionsInterfaceIsCurrent += OptionsInterfaceIsCurrent;
        _pauseMenuInputs.OnMainInterfaceIsCurrent += MainInterfaceIsCurrent;
        _pauseMenuInputs.OnControlsInterfaceIsCurrent += ControlsInterfaceIsCurrent;
        _pauseMenuInputs.OnAudioInterfaceIsCurrent += AudioInterfaceIsCurrent;

        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnPauseMenuIsOutOfScreen += ResetInterfaceOnMenuIsOutOfScreen;

        _animator = GetComponent<Animator>();

        _currentInterface = "Main";
    }

    private void ResetInterfaceOnMenuIsOutOfScreen()
    {
        MainInterfaceIsCurrent("ShowMainInterface");
    }

    private void OptionsInterfaceIsCurrent(string current)
    {
        if (current != _currentInterface && _currentInterface == "Main")
        {
            _animator.SetTrigger("OptionsSlideIn");
            _currentInterface = "Options";
            OnMainInterfaceFade();
        }
        else if (current != _currentInterface && _currentInterface == "Controls")
        {
            _animator.SetTrigger("OptionsSlideIn");
            _currentInterface = "Options";
            OnControlsInterfaceFade();
        }
        else if (current != _currentInterface && _currentInterface == "Audio")
        {
            _animator.SetTrigger("OptionsSlideIn");
            _currentInterface = "Options";
            OnAudioInterfaceFade();
        }
    }

    private void MainInterfaceIsCurrent(string current)
    {
        if (current != _currentInterface)
        {
            if (current == "ShowMainInterface")
            {
                _animator.SetTrigger(current);
                _currentInterface = "Main";
            }
            else
            {
                _animator.SetTrigger("MainSlideIn");
                _currentInterface = "Main";
                OnOptionsInterfaceFade();
            }
        }
    }

    private void ControlsInterfaceIsCurrent(string current)
    {
        if (current != _currentInterface && _currentInterface == "Options")
        {
            _animator.SetTrigger("ControlsSlideIn");
            _currentInterface = "Controls";
            OnOptionsInterfaceFade();
        }
    }

    private void AudioInterfaceIsCurrent(string current)
    {
        if (current != _currentInterface && _currentInterface == "Options")
        {
            _animator.SetTrigger("AudioSlideIn");
            _currentInterface = "Audio";
            OnOptionsInterfaceFade();
        }
    }
}