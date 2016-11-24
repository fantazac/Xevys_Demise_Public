using UnityEngine;
using System.Collections;
using System.Linq;

public class KnifeHitWall : MonoBehaviour
{
    [SerializeField]
    private string[] _canHitObjects;

    private BoxCollider2D _hitbox;
    private Rigidbody2D _rigidbody;
    private DestroyPlayerProjectile _destroyProjectile;

    private void Start()
    {
        _hitbox = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _destroyProjectile = GetComponent<DestroyPlayerProjectile>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHitObject(collider))
        {
            StopKnife();
        }
    }

    private void StopKnife()
    {
        _hitbox.isTrigger = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
        _destroyProjectile.TouchedWall();
    }

    private bool CanHitObject(Collider2D collider)
    {
        return _canHitObjects.Contains(collider.gameObject.tag);
    }
}
