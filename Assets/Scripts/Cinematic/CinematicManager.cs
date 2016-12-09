using UnityEngine;
using System.Collections;

public class CinematicManager : MonoBehaviour
{
    private PauseGame _pauseGame;

    public bool CinematicIsPlaying { get; set; }

    public bool MoveCameraToCinematic { get; private set; }
    public Vector3 CinematicPosition { get; private set; }

    private void Start()
    {
        _pauseGame = StaticObjects.GetPause().GetComponent<PauseGame>();
        MoveCameraToCinematic = false;
    }

    public void StartCinematic(Vector3 position, float duration)
    {
        MoveCameraToCinematic = true;
        CinematicPosition = position;

        StartCoroutine(SendCameraBackToPlayer(duration));
    }

    private IEnumerator SendCameraBackToPlayer(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        _pauseGame.Pause();
        MoveCameraToCinematic = false;
        CinematicIsPlaying = false;
        CinematicPosition = Vector3.zero;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
