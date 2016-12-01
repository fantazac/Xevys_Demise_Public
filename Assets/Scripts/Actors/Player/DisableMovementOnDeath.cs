using UnityEngine;
using System.Collections;

public class DisableMovementOnDeath : MonoBehaviour
{
    private PlayerMovement[] _playerMovements;

    private void Start()
    {
        _playerMovements = GetComponents<PlayerMovement>();

        GetComponent<Health>().OnDeath += DisableMovement;
    }

    private void DisableMovement()
    {
        foreach(PlayerMovement playerMovement in _playerMovements)
        {
            playerMovement.enabled = false;
        }
    }
}
