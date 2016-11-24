using UnityEngine;
using System.Collections;
using System.Linq;

public class OnThrowingWeaponHit : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    [SerializeField]
    protected string[] _canHitTags;

    private DestroyPlayerProjectile _destroyProjectile;

    private void Start()
    {
        _destroyProjectile = GetComponent<DestroyPlayerProjectile>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHitEntity(collider) && CanAttackEnemy(collider))
        {
            collider.GetComponent<Health>().Hit(_baseDamage, Vector2.zero);
            _destroyProjectile.DestroyNow = true;
        }
    }

    private bool CanAttackEnemy(Collider2D collider)
    {
        return CanAttackNormalEnemy(collider) || CanAttackBoss(collider);
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
