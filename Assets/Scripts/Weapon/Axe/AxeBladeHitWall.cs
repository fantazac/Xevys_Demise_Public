using UnityEngine;
using System.Collections;
using System.Linq;

public class AxeBladeHitWall : WeaponHitWall
{
    private RotateAxe _rotateAxe;

    private const float TIME_BEFORE_KINEMATIC = 0.05f;

    private WaitForSeconds _delayDisableRigidbody;

    protected override void Start()
    {
        _delayDisableRigidbody = new WaitForSeconds(TIME_BEFORE_KINEMATIC);

        _rotateAxe = GetComponentInParent<RotateAxe>();
        _hitbox = GetComponent<PolygonCollider2D>();
        base.Start();
    }

    protected override void Stop()
    {
        _rotateAxe.CanRotate = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
        _hitbox.isTrigger = false;
        _destroyProjectile.TouchedWall();

        StartCoroutine(DisableRigidbody());
    }

    //Cette coroutine existe car la axe doit sortir du mur avant qu'on l'immobilise 
    private IEnumerator DisableRigidbody()
    {
        yield return _delayDisableRigidbody;

        _rigidbody.isKinematic = true;
    }
}


