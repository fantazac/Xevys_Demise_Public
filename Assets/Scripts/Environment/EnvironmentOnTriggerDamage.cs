using UnityEngine;
using System.Collections;

public class EnvironmentOnTriggerDamage : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private Health _health;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (CanAttackPlayer(collider))
        {
            _health.Hit(_baseDamage, transform.position);
        }
    }

    private bool CanAttackPlayer(Collider2D collider)
    {
        return !StaticObjects.GetPlayerState().IsInvincible && collider.gameObject.tag == StaticObjects.GetObjectTags().Player && 
            !_inventoryManager.FireProofArmorEnabled;
    }
}
