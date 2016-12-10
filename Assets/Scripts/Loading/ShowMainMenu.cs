using UnityEngine;
using Prime31.TransitionKit;

public class ShowMainMenu : MonoBehaviour
{
    public void ShowMenu()
    {
        var fader = new FadeTransition()
        {
            nextScene = 1,
            fadedDelay = 0f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }
}
