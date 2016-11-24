﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenuAnimationManager : MonoBehaviour
{

    public delegate void OnFadeTriggerHandler(string fade);
    public event OnFadeTriggerHandler OnFade;

    public delegate void OnPauseMenuStateChangedHandler(bool isActive);
    public event OnPauseMenuStateChangedHandler OnPauseMenuStateChanged;

    private PauseMenuInputs _pauseMenuInputs;
    private Animator _slideAnimator;
    private bool _active;

    private void Start()
    {
        _pauseMenuInputs = GetComponentInChildren<PauseMenuInputs>();
        _pauseMenuInputs.TriggerAnimations += AnimatePauseMenu;
        _slideAnimator = GetComponent<Animator>();
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
    }

    private void SlideOut()
    {
        _slideAnimator.SetTrigger("SlideOut");
        _active = false;
        _pauseMenuInputs.CanSlide = false;
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
