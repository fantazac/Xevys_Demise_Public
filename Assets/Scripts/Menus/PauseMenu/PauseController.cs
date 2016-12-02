using UnityEngine;

public class PauseController : MonoBehaviour
{
    private PauseMenuInputs _pauseMenuInputs;
    private PauseGame _pauseGame;

    private void Start()
    {
        _pauseGame = StaticObjects.GetPause().GetComponent<PauseGame>();
        _pauseMenuInputs = GetComponent<PauseMenuInputs>();

        _pauseMenuInputs.TriggerAnimations += ChangePauseState;
    }

    private void ChangePauseState(bool isDead)
    {
        _pauseGame.Pause();
    }
}
