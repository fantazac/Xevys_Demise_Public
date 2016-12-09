using UnityEngine;
using Prime31.TransitionKit;

public class ShowAchievementsScene : MonoBehaviour
{
    private MainMenuInputs _mainMenuInputs;
    
    private void Start()
    {
        _mainMenuInputs = MainMenuStaticObjects.GetMainMenuPanel().GetComponentInChildren<MainMenuInputs>();
        _mainMenuInputs.OnAchievementsButtonPressed += ShowAchievements;
    }

    private void ShowAchievements()
    {
        var fader = new FadeTransition()
        {
            nextScene = 5,
            fadedDelay = 0f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }
}
