using UnityEngine;
using System.Collections;

public class PauseMenuAnimationManager : MonoBehaviour
{

    public delegate void OnFadeTriggerHandler(string fade);
    public event OnFadeTriggerHandler OnFade;

    private PauseMenuInputs _pauseMenuInputs;
    private Animator _slideAnimator;
    private bool _active;

    private void Start()
    {
        _pauseMenuInputs = GetComponent<PauseMenuInputs>();
        _pauseMenuInputs.TriggerAnimations += AnimatePauseMenu;
        _slideAnimator = GetComponent<Animator>();
        _active = false;
    }

    private void AnimatePauseMenu()
    {
        if (!_active)
        {
            SlideIn();
            FadeIn();
        }
        else
        {
            SlideOut();
            FadeOut();
        }
    }

    private void SlideIn()
    {
        _slideAnimator.SetTrigger("SlideIn");
        _active = true;
    }

    private void EnableIdleAnimation()
    {
        _slideAnimator.SetTrigger("Idle");
        _slideAnimator.SetTrigger("Unactive");
    }

    private void SlideOut()
    {
        _slideAnimator.SetTrigger("SlideOut");
        _active = false;
    }

    private void FadeIn()
    {
        OnFade("FadeIn");
    }

    private void FadeOut()
    {
        OnFade("FadeOut");
    }
}
