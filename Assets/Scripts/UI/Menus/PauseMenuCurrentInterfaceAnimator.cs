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

    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private Animator _animator;

    private string _currentInterface;

    private WaitForSeconds _waitForZeroPointOneSeconds;

    private void Start()
    {
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnOptionsInterfaceIsCurrent += OptionsInterfaceIsCurrent;
        _pauseMenuAnimationManager.OnMainInterfaceIsCurrent += MainInterfaceIsCurrent;
        _pauseMenuAnimationManager.OnControlsInterfaceIsCurrent += ControlsInterfaceIsCurrent;
        _pauseMenuAnimationManager.OnAudioInterfaceIsCurrent += AudioInterfaceIsCurrent;

        _animator = GetComponent<Animator>();

        _currentInterface = "Main";

        _waitForZeroPointOneSeconds = new WaitForSeconds(0.01f);
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
            StartCoroutine("FadeControlsButtonsWithDelay");
        }
    }

    private void MainInterfaceIsCurrent(string current)
    {
        if (current != _currentInterface)
        {
            _animator.SetTrigger("MainSlideIn");
            _currentInterface = "Main";
            OnOptionsInterfaceFade();
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
            StartCoroutine("FadeControlsButtonsWithDelay");
        }
    }

    private IEnumerator FadeControlsButtonsWithDelay()
    {
        yield return _waitForZeroPointOneSeconds;

        OnControlsInterfaceFade();
    }
}