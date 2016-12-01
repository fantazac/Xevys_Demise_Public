using UnityEngine;

public class SpawnToCheckpointAfterDeath : MonoBehaviour
{
    [SerializeField]
    private Vector3 centralCheckpoint;

    [SerializeField]
    private Vector3[] checkpoints;

    private int _lastCheckpoint = 0;

    private Health _playerHealth;
    private PlayerGroundMovement _playerGroundMovement;
    private Animator _animator;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
        _animator = GetComponentInChildren<Animator>();
        _playerGroundMovement = GetComponent<PlayerGroundMovement>();
    }

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
        _playerHealth.FullHeal();
        _playerGroundMovement.enabled = true;
        _animator.SetBool(StaticObjects.GetAnimationTags().IsDying, false);
    }
}