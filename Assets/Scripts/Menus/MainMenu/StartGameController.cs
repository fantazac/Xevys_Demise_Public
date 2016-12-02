using UnityEngine;
using Prime31.TransitionKit;

public class StartGameController : MonoBehaviour
{
    private MainMenuInputs _mainMenuInputs;

    void Start()
    {
        _mainMenuInputs = GetComponent<MainMenuInputs>();
        _mainMenuInputs.OnStartButtonPressed += StartGame;
    }

    private void StartGame()
    {
        AsyncOperation async = Application.LoadLevelAsync("LoadingScreen");
        /*var fader = new FadeTransition()
        {
            nextScene = 2,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);*/
    }
}
