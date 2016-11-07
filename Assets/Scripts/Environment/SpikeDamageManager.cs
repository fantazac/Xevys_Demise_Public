using UnityEngine;
using System.Collections;

//Cela ressemble à EnemiesAttackManager, à l'exception que celui-ci vérifie le dommage au niveau de la Collision, plutôt que sur le Trigger.
//Nous avons donc préféré les garder en scripts séparés.
public class SpikeDamageManager : MonoBehaviour
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CanAttackPlayer(collision))
        {
            _health.Hit(_baseDamage);
            _knockback.KnockbackPlayer(transform.position);
        }
    }

    private bool CanAttackPlayer(Collision2D collision)
    {
        return !PlayerState.IsInvincible && collision.gameObject.tag == "Player";
    }
}
