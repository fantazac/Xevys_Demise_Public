using System.Collections;
using Prime31.TransitionKit;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private int scene = 3;

    public void Start()
    {
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(0f);
        var fader = new FadeTransition()
        {
            nextScene = scene,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }
}
