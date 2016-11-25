using UnityEngine;

public class SpawnToCheckpointAfterDeath : MonoBehaviour
{
    [SerializeField] private Vector3 centralCheckpoint;
    [SerializeField] private Vector3[] checkpoints;
    private int _lastCheckpoint = 0;

    public void SaveCheckpoint(int checkPoint)
    {
        if (checkPoint != 0)
        {
            _lastCheckpoint = checkPoint;
        }
    }

    public void SpawnToCentralCheckpoint()
    {
        transform.position = centralCheckpoint;
        RestartPlayer();
    }

    public void SpawnToLastCheckpoint()
    {
        if (_lastCheckpoint > 0)
        {
            transform.position = checkpoints[_lastCheckpoint-1];
            RestartPlayer();
        }
        else
        {
            SpawnToCentralCheckpoint();
        }
    }

    private void RestartPlayer()
    {
        GetComponent<Health>().FullHeal();
        GetComponentInChildren<Animator>().SetBool(StaticObjects.GetAnimationTags().IsDying, false);
    }
}