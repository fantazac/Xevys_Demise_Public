using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenuAnimationManager : MonoBehaviour
{

    public delegate void OnFadeTriggerHandler(string fade);
    public event OnFadeTriggerHandler OnFade;

    public delegate void OnPauseMenuStateChangedHandler(bool isActive);
    public event OnPauseMenuStateChangedHandler OnPauseMenuStateChanged;

    public delegate void OnMainInterfaceIsCurrentHandler(string current);
    public event OnMainInterfaceIsCurrentHandler OnMainInterfaceIsCurrent;

    public delegate void OnOptionsInterfaceIsCurrentHandler(string current);
    public event OnOptionsInterfaceIsCurrentHandler OnOptionsInterfaceIsCurrent;

    public delegate void OnControlsInterfaceIsCurrentHandler(string current);
    public event OnControlsInterfaceIsCurrentHandler OnControlsInterfaceIsCurrent;

    public delegate void OnAudioInterfaceIsCurrentHandler(string current);
    public event OnAudioInterfaceIsCurrentHandler OnAudioInterfaceIsCurrent;

    private PauseMenuInputs _pauseMenuInputs;
    private Animator _slideAnimator;
    private EventSystem _pauseMenuEventSystem;
    private bool _active;

    private void Start()
    {
        _pauseMenuInputs = GetComponentInChildren<PauseMenuInputs>();
        _pauseMenuInputs.TriggerAnimations += AnimatePauseMenu;
        _slideAnimator = GetComponent<Animator>();
        _pauseMenuEventSystem = EventSystem.current;
        _active = false;
    }

    private void SlideOutAnimationEnded()
    {
        _pauseMenuInputs.CanSlide = true;
    }

    private void SlideInAnimationEnded()
    {
        _pauseMenuInputs.CanSlide = true;
    }

    private void AnimatePauseMenu()
    {
        if (!_active)
        {
            SlideIn();
            FadeIn();
            OnPauseMenuStateChanged(_active);
        }
        else
        {
            SlideOut();
            FadeOut();
            OnPauseMenuStateChanged(_active);
        }
    }

    private void SlideIn()
    {
        _slideAnimator.SetTrigger("SlideIn");
        _active = true;
        _pauseMenuInputs.CanSlide = false;
        StartCoroutine("ShowAppropriateMenuInterface");
    }

    private void SlideOut()
    {
        _slideAnimator.SetTrigger("SlideOut");
        _active = false;
        _pauseMenuInputs.CanSlide = false;
        StopCoroutine("ShowAppropriateMenuInterface");
    }

    private void FadeIn()
    {
        OnFade("FadeIn");
    }

    private void FadeOut()
    {
        OnFade("FadeOut");
    }

    private IEnumerator ShowAppropriateMenuInterface()
    {
        while (true)
        {
            if (_pauseMenuEventSystem.currentSelectedGameObject != null)
            {
                switch (_pauseMenuEventSystem.currentSelectedGameObject.name)
                {
                    case "ResumeBtn":
                    case "OptionBtn":
                    case "QuitBtn":
                        OnMainInterfaceIsCurrent("Main");
                        break;
                    case "OptionsBackBtn":
                    case "ControlsOptionsBtn":
                    case "AudioOptionsBtn":
                        OnOptionsInterfaceIsCurrent("Options");
                        break;
                    case "ControlsBackBtn":
                    case "KeyboardSchemeSwitch":
                    case "GamepadSchemeSwitch":
                        //OnControlsInterfaceIsCurrent();
                        break;
                    case "AudioBackBtn":
                    case "MusicVolumeSldr":
                    case "FXVolumeSldr":
                    case "MusicSwitch":
                        //OnAudioInterfaceIsCurrent();
                        break;
                }
            }
            yield return null;
        }
    }
}
