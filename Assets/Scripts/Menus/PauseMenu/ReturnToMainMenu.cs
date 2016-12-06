using UnityEngine;
using Prime31.TransitionKit;

public class ReturnToMainMenu : MonoBehaviour {

    private PauseMenuInputs _pauseMenuInputs;

    void Start()
    {
        _pauseMenuInputs = GetComponent<PauseMenuInputs>();
        _pauseMenuInputs.OnReturnToMenuButtonPressed += ToMainMenu;
    }

    private void ToMainMenu()
    {
        var fader = new FadeTransition()
        {
            nextScene = 1,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }
}
