using UnityEngine;
using System.Collections;

public class AxeHandleHitWall : MonoBehaviour
{

    private const float BASE_AXE_DRAG = 5f;

    private RotateAxe _rotateAxe;
    private Rigidbody2D _rigidbody;
    private PolygonCollider2D _hitbox;
    private PolygonCollider2D _bladeHitbox;
    private DestroyPlayerProjectile _destroyProjectile;

    public bool TouchesGround { get; set; }

    private void Start()
    {
        TouchesGround = false;

        _rotateAxe = GetComponentInParent<RotateAxe>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _hitbox = GetComponent<PolygonCollider2D>();
        _bladeHitbox = transform.parent.GetComponentsInChildren<PolygonCollider2D>()[0];
        _destroyProjectile = GetComponentInParent<DestroyPlayerProjectile>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "LevelWall" || collider.gameObject.tag == "Spike"
            || (collider.gameObject.tag == "FlyingPlatform" && _rigidbody.velocity.y < 0))
        {
            _rotateAxe.Rotate = false;
            _hitbox.isTrigger = false;
            _bladeHitbox.isTrigger = false;
            _rigidbody.drag = BASE_AXE_DRAG;
            TouchesGround = true;
            _destroyProjectile.TouchesGround = true;
        }
    }
}
