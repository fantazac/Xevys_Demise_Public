using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class WeaponHitWall : MonoBehaviour
{

    [SerializeField]
    protected string[] _canHitObjects;

    [SerializeField]
    protected bool _canHitFlyingPlatform = false;

    protected Rigidbody2D _rigidbody;
    protected DestroyPlayerProjectile _destroyProjectile;
    protected Collider2D _hitbox;

    protected const float FLYING_PLATFORM_OFFSET = 0.15f;

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _destroyProjectile = GetComponent<DestroyPlayerProjectile>();
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHitObject(collider))
        {
            Stop();
        }
    }

    protected virtual void Stop() { }

    protected virtual bool CanHitObject(Collider2D collider)
    {
        return _canHitObjects.Contains(collider.gameObject.tag) || IsFlyingPlatform(collider);
    }

    protected bool IsFlyingPlatform(Collider2D collider)
    {
        return _canHitFlyingPlatform && collider.gameObject.tag == StaticObjects.GetObjectTags().FlyingPlatform
            && _rigidbody.velocity.y < 0 && transform.position.y > collider.transform.position.y + FLYING_PLATFORM_OFFSET;
    }

}
