using UnityEngine;
using System.Collections;

public class SpawnToCheckpointAfterDeath : MonoBehaviour
{
    [SerializeField]
    Vector3[] checkpoints;
    private int _lastCheckpoint = 0;

    private void Start()
    {
        GetComponent<PlayDeathAnimation>().OnDyingAnimationFinished += SpawnToCheckpoint;
    }

    public void SaveCheckpoint(int _checkPoint)
    {
        _lastCheckpoint = _checkPoint;
    }

    private void SpawnToCheckpoint()
    {
        transform.position = checkpoints[_lastCheckpoint];
        GetComponent<Health>().FullHeal();
        GetComponentInChildren<Animator>().SetBool("IsDying", false);
    }
}
