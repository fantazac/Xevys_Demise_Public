using UnityEngine;
using System.Collections;

public class EnemiesAttackManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private Health _health;
    private KnockbackOnDamageTaken _knockback;

    private void Start()
    {
        _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _knockback = StaticObjects.GetPlayer().GetComponent<KnockbackOnDamageTaken>();
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
        return !PlayerState.IsInvincible && collider.gameObject.tag == "Player";
    }
}
