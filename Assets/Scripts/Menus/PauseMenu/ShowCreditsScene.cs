﻿using UnityEngine;
using Prime31.TransitionKit;

public class ShowCreditsScene : MonoBehaviour
{
    public void ShowCredits()
    {
        var fader = new FadeTransition()
        {
            nextScene = 4,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }
}
