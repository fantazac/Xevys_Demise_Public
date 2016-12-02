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

    private CinematicManager _cinematicManager;

    private void Start()
    {
        _cinematicManager = StaticObjects.GetCinematic().GetComponent<CinematicManager>();
        GetComponent<ActivateTrigger>().OnTrigger += StartCinematic;
    }

    private void StartCinematic()
    {
        StartCoroutine(PlayCinematicAfterInitialDelay());
    }

    private IEnumerator PlayCinematicAfterInitialDelay()
    {
        _cinematicManager.CinematicIsPlaying = true;
        
        yield return new WaitForSecondsRealtime(_timeBeforeCinematic);
        
        _cinematicManager.StartCinematic(_cinematicPosition, _cinematicDuration);
    }

}
