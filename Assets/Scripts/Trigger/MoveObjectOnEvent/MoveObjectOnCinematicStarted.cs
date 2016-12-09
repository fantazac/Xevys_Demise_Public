using UnityEngine;
using System.Collections;

public class MoveObjectOnCinematicStarted : MoveObjectOnEvent
{
    [SerializeField]
    private GameObject _triggerActivationObject;

    [SerializeField]
    private float _timeBeforeCinematicStart = 1f;

    public delegate void OnStartedMovingHandler();
    public event OnStartedMovingHandler OnStartedMoving;

    protected override void Start()
    {
        _triggerActivationObject.GetComponent<ActivateTrigger>().OnTrigger += StartObjectMovement;

        base.Start();
    }

    protected override void StartObjectMovement()
    {
        StartCoroutine(StartMovementWhenCinematicIsRunning());
    }

    private IEnumerator StartMovementWhenCinematicIsRunning()
    {
        yield return new WaitForSecondsRealtime(_timeBeforeCinematicStart);
        
        base.StartObjectMovement();
        
        if (OnStartedMoving != null)
        {
            OnStartedMoving();
        }
    }

    protected override void DoMovement()
    {
        gameObject.transform.position += _directionalVector * _speedInUnitsPerSecond * Time.unscaledDeltaTime;
        _distanceMade += _speedInUnitsPerSecond * Time.unscaledDeltaTime;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
