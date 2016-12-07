using UnityEngine;
using System.Collections;
using System.Linq;

public class AxeHandleHitWall : WeaponHitWall
{
    [SerializeField]
    private float _axeDrag = 5f;

    private RotateAxe _rotateAxe;
    private PolygonCollider2D _bladeHitbox;

    protected override void Start()
    {
        _rotateAxe = GetComponentInParent<RotateAxe>();
        _hitbox = GetComponent<PolygonCollider2D>();
        _bladeHitbox = transform.parent.GetComponentsInChildren<PolygonCollider2D>()[0];
        base.Start();
    }

    protected override void Stop()
    {
        _rotateAxe.CanRotate = false;
        _hitbox.isTrigger = false;
        _bladeHitbox.isTrigger = false;
        _rigidbody.drag = _axeDrag;

        if (_destroyProjectile != null)
        {
            _destroyProjectile.TouchedWall();
        }
    }
}
