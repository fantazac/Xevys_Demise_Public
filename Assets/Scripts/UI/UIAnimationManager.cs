﻿using UnityEngine;
using System.Collections;

public class UIAnimationManager : MonoBehaviour
{
    private PauseMenuInputs _pauseMenuInputs;

    private Animator _animator;
    private bool _active;

    private void Start()
    {
        _pauseMenuInputs = StaticObjects.GetPauseMenuPanel().GetComponentInChildren<PauseMenuInputs>();
        _animator = GetComponent<Animator>();
        _pauseMenuInputs.TriggerAnimations += FadeUI;
        _active = true;
    }

    private void FadeUI()
    {
        if (!_active)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    private void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
        _active = false;
    }

    private void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
        _active = true;
    }
}