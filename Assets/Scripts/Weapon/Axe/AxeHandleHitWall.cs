using UnityEngine;
using System.Collections;
using System.Linq;

public class AxeHandleHitWall : MonoBehaviour
{
    [SerializeField]
    private float _axeDrag = 5f;

    [SerializeField]
    private bool _canHitFlyingPlatform = false;

    [SerializeField]
    private string[] _canHitObjects;

    private RotateAxe _rotateAxe;
    private Rigidbody2D _rigidbody;
    private PolygonCollider2D _hitbox;
    private PolygonCollider2D _bladeHitbox;
    private DestroyPlayerProjectile _destroyProjectile;

    private const float FLYING_PLATFORM_OFFSET = 0.15f;

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
        if (CanHitObject(collider))
        {
            DragAxeOnFloor();
        }
    }

    private void DragAxeOnFloor()
    {
        _rotateAxe.CanRotate = false;
        _hitbox.isTrigger = false;
        _bladeHitbox.isTrigger = false;
        _rigidbody.drag = _axeDrag;
        TouchesGround = true;
        _destroyProjectile.TouchesWall = true;
    }

    private bool CanHitObject(Collider2D collider)
    {
        return _canHitObjects.Contains(collider.gameObject.tag) || IsFlyingPlatform(collider);
    }

    private bool IsFlyingPlatform(Collider2D collider)
    {
        return _canHitFlyingPlatform && collider.gameObject.tag == StaticObjects.GetUnityTags().FlyingPlatform 
            && _rigidbody.velocity.y < 0 && transform.position.y > collider.transform.position.y + FLYING_PLATFORM_OFFSET;
    }
}
