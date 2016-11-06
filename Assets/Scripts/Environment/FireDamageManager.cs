using UnityEngine;
using System.Collections;

public class FireDamageManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private Health _health;
    private KnockbackOnDamageTaken _knockback;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _knockback = StaticObjects.GetPlayer().GetComponent<KnockbackOnDamageTaken>();
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (CanAttackPlayer(collider))
        {
            _health.Hit(_baseDamage);
            _knockback.KnockbackPlayer(transform.position);
        }
    }

    private bool CanAttackPlayer(Collider2D collider)
    {
        return !PlayerState.IsInvincible && collider.gameObject.tag == "Player" && !_inventoryManager.FireProofArmorEnabled;
    }
}
