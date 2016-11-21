using UnityEngine;

public class PauseController : MonoBehaviour
{
    private PauseMenuInputs _pauseMenuInputs;
    private bool _gameIsPaused;

    private void Start()
    {
        _pauseMenuInputs = GetComponent<PauseMenuInputs>();
        _gameIsPaused = false;

        _pauseMenuInputs.TriggerAnimations += ChangePauseState;
    }

    private void ChangePauseState()
    {
        Time.timeScale = _gameIsPaused ? 1 : 0;
        _gameIsPaused = !_gameIsPaused;
    }
}
