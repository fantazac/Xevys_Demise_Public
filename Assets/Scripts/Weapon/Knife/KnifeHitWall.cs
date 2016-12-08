using UnityEngine;
using System.Collections;
using System.Linq;

public class KnifeHitWall : WeaponHitWall
{
    protected override void Start()
    {
        _hitbox = GetComponent<BoxCollider2D>();
        base.Start();
    }

    protected override void Stop()
    {
        _hitbox.isTrigger = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
        _destroyProjectile.TouchedWall();
    }

    protected override bool CanHitObject(Collider2D collider)
    {
        return _canHitObjects.Contains(collider.gameObject.tag);
    }
}
