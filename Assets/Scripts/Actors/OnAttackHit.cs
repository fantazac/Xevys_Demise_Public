using UnityEngine;
using System.Collections;
using System.Linq;

public class OnAttackHit : MonoBehaviour
{
    [SerializeField]
    protected int _baseDamage = 100;

    [SerializeField]
    protected string[] _canHitTags;

    protected Health _playerHealth;

    protected bool _isPlayer = false;

    protected virtual void Start()
    {
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        if (tag == StaticObjects.GetObjectTags().BasicAttackHitbox)
        {
            _isPlayer = true; 
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHitEntity(collider) && CanAttackEnemy(collider))
        {
            collider.GetComponent<Health>().Hit(_baseDamage, Vector2.zero); 
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        if (CanHitEntity(collider) && CanAttackPlayer(collider))
        {
            _playerHealth.Hit(_baseDamage, transform.position);
        }
    }

    protected bool CanAttackPlayer(Collider2D collider)
    {
        return !_isPlayer && !StaticObjects.GetPlayerState().IsInvincible;
    }

    private bool CanAttackEnemy(Collider2D collider)
    {
        return _isPlayer && (CanAttackNormalEnemy(collider) || CanAttackBoss(collider));
    }

    private bool CanAttackNormalEnemy(Collider2D collider)
    {
        return !collider.GetComponent<EnemyType>().IsABoss;
    }

    private bool CanAttackBoss(Collider2D collider)
    {
        return collider.GetComponent<EnemyType>().IsABoss && collider is PolygonCollider2D;
    }

    protected bool CanHitEntity(Collider2D collider)
    {
        return _canHitTags.Contains(collider.gameObject.tag);
    }
}
