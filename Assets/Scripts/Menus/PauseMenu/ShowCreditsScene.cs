using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class ShowCreditsScene : MonoBehaviour
{
    public void ShowCredits()
    {
        SceneManager.LoadSceneAsync(4);
        //var fader = new FadeTransition()
        //{
        //    nextScene = 4,
        //    fadedDelay = 0.2f,
        //    fadeToColor = Color.black
        //};
        //TransitionKit.instance.transitionWithDelegate(fader);
    }
}
