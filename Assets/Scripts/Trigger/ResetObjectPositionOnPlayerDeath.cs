using UnityEngine;
using System.Collections;

public class ResetObjectPositionOnPlayerDeath : MonoBehaviour
{
    private Vector3 _initialPosition;

    private Health _playerHealth;

    private void Start()
    {
        _initialPosition = transform.position;

        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();

        _playerHealth.OnDeath += ResetPosition;
    }

    private void ResetPosition()
    {
        transform.position = _initialPosition;
    }

    private void OnDestroy()
    {
        _playerHealth.OnDeath -= ResetPosition;
    }
}
