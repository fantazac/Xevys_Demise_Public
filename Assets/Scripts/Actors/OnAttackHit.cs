using UnityEngine;
using System.Collections;
using System.Linq;

public class OnAttackHit : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    [SerializeField]
    private string[] _canHitTags;

    private Health _playerHealth;

    private bool _isPlayer = true;

    private void Start()
    {
        if(tag != "BasicAttackHitbox")
        {
            _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
            _isPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHitEntity(collider) && CanAttackEnemy(collider))
        {
            collider.GetComponent<Health>().Hit(_baseDamage, Vector2.zero);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (CanHitEntity(collider) && CanAttackPlayer(collider))
        {
            _playerHealth.Hit(_baseDamage, transform.position);
        }
    }

    private bool CanAttackPlayer(Collider2D collider)
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

    private bool CanHitEntity(Collider2D collider)
    {
        return _canHitTags.Contains(collider.gameObject.tag);
    }
}
