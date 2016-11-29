using UnityEngine;
using System.Collections;

public class DisableOnEntityDeath : MonoBehaviour
{

    [SerializeField]
    private GameObject _breakableItem;

    [SerializeField]
    private bool _disableObject = false;

    private Health _entityHealth;
    private BoxCollider2D _hitbox;

    private void Start()
    {
        _hitbox = GetComponent<BoxCollider2D>();
        _breakableItem.GetComponent<SpawnBossOnBreakableItemDestroyed>().OnBossSpawn += SetEntity;
    }

    private void SetEntity(GameObject entity)
    {
        _entityHealth = entity.GetComponent<Health>();
        if (_disableObject)
        {
            _entityHealth.OnDeath += DisableObject;
        }
        else
        {
            _entityHealth.OnDeath += DisableHitbox;
        }
    }

    private void DisableHitbox()
    {
        _hitbox.enabled = false;
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
