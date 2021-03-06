﻿using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;

public class LoadMenu : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(WaitForThreeSeconds());
    }

    private IEnumerator WaitForThreeSeconds()
    {
        yield return new WaitForSeconds(3f);

        var fader = new FadeTransition()
        {
            nextScene = 1,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
