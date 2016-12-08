using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;

public class WaitAndReturnToMenu : MonoBehaviour
{
    public void OnScrollingEnded()
    {
        StartCoroutine(WaitForTwoSeconds());
    }

    private IEnumerator WaitForTwoSeconds()
    {
        yield return new WaitForSeconds(2f);

        var fader = new FadeTransition()
        {
            nextScene = 1,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }

}
