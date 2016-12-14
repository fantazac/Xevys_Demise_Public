using UnityEngine;
using System.Collections;

public class PlayCinematicOnTrigger : MonoBehaviour
{
    [SerializeField]
    private Vector3 _cinematicPosition;

    [SerializeField]
    private float _timeBeforeCinematic = 1f;

    [SerializeField]
    private float _cinematicDuration = 3f;

    private PauseGame _pauseGame;

    private CinematicManager _cinematicManager;

    private bool _canPlayCinematic = true;

    private void Start()
    {
        _pauseGame = StaticObjects.GetPause().GetComponent<PauseGame>();
        _cinematicManager = StaticObjects.GetCinematic().GetComponent<CinematicManager>();
        GetComponent<ActivateTrigger>().OnTrigger += StartCinematic;
    }

    public void DisableCinematicOnReload()
    {
        _canPlayCinematic = false;
    }

    private void StartCinematic()
    {
        if (_canPlayCinematic)
        {
            _pauseGame.Pause();
            _cinematicManager.CinematicIsPlaying = true;
            StartCoroutine(PlayCinematicAfterInitialDelay());
        }
    }

    private IEnumerator PlayCinematicAfterInitialDelay()
    {
        yield return new WaitForSecondsRealtime(_timeBeforeCinematic);

        _cinematicManager.StartCinematic(_cinematicPosition, _cinematicDuration);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
