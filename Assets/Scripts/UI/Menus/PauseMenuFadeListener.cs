using UnityEngine;
using System.Collections;

public class PauseMenuFadeListener : MonoBehaviour
{
    [SerializeField]
    private PauseMenuAnimationManager _pauseMenuAnimationManager;

    void Start()
    {
        _pauseMenuAnimationManager.OnFade += Fade;
    }

    private void Fade(string fading)
    {
        GetComponent<Animator>().SetTrigger(fading);
    }
}
