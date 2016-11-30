using UnityEngine;
using System.Collections;

public class MoveObjectOnEntityDestroyed : MoveObjectOnEvent
{
    [SerializeField]
    private GameObject _breakableItem;

    private Health _entityHealth;

    protected override void Start()
    {
        _breakableItem.GetComponent<SpawnBossOnBreakableItemDestroyed>().OnBossSpawn += SetMovementOnEntityDeath;

        base.Start();
    }

    private void SetMovementOnEntityDeath(GameObject entity)
    {
        _entityHealth = entity.GetComponent<Health>();
        if(_entityHealth == null)
        {
            _entityHealth = entity.GetComponentInChildren<Health>();
        }
        _entityHealth.OnDeath += StartObjectMovement;
    }
}
