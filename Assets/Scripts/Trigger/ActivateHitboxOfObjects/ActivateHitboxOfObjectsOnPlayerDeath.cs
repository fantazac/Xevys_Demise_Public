using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnPlayerDeath : ActivateHitboxOfObjects
{
    private Health _playerHealth;

    protected void Start()
    {
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _playerHealth.OnDeath += Activate;
    }

    protected void OnDestroy()
    {
        _playerHealth.OnDeath -= Activate;
    }
}
