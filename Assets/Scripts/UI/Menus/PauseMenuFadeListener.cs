using UnityEngine;
using System.Collections;

public class PauseMenuFadeListener : MonoBehaviour
{
    private PauseMenuAnimationManager _pauseMenuAnimationManager;

    void Start()
    {
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnFade += Fade;
    }

    private void Fade(string fading)
    {
        GetComponent<Animator>().SetTrigger(fading);
    }
}
