using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnEntityDeath : ActivateHitboxOfObjects
{

    [SerializeField]
    private GameObject _breakableItem;

    private void Start()
    {
        _breakableItem.GetComponent<SpawnBossOnBreakableItemDestroyed>().OnBossSpawn += SetEntity;
    }

    private void SetEntity(GameObject entity)
    {
        entity.GetComponent<Health>().OnDeath += Activate;
    }

}
