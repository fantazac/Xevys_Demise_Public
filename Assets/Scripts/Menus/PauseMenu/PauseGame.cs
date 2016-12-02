using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{
    private bool _gameIsPaused = false;

    public void Pause()
    {
        Time.timeScale = _gameIsPaused ? 1 : 0;
        _gameIsPaused = !_gameIsPaused;
    }
}
