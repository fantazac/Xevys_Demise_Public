using UnityEngine;
using System.Collections;

public class DisableDoorResetOnPlayerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _door;

    private ResetObjectPositionOnPlayerDeath _resetPosition;

    private void Start()
    {
        _resetPosition = _door.GetComponent<ResetObjectPositionOnPlayerDeath>();
        GetComponent<SpawnBossOnBreakableItemDestroyed>().OnBossFightFinished += DisableReset;
    }

    private void DisableReset()
    {
        _resetPosition.DisableEvent();
    }
}
