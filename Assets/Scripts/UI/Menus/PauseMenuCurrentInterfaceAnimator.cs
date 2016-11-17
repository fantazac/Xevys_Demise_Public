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

    private void Start()
    {
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnOptionsInterfaceIsCurrent += OptionsInterfaceIsCurrent;
        _pauseMenuAnimationManager.OnMainInterfaceIsCurrent += MainInterfaceIsCurrent;

        _animator = GetComponent<Animator>();

        _currentInterface = "Main";
    }

    private void OptionsInterfaceIsCurrent(string current)
    {
        if (current != _currentInterface)
        {
            _animator.SetTrigger("OptionsSlideIn");
            _currentInterface = "Options";
            OnMainInterfaceFade();
        }
    }

    private void MainInterfaceIsCurrent(string current)
    {
        if (current != _currentInterface)
        {
            _animator.SetTrigger("MainSlideIn");
            _currentInterface = "Main";
            //OnOptionsInterfaceFade();
        }
    }
}