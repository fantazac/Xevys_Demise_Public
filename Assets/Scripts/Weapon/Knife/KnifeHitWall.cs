using UnityEngine;
using System.Collections;

public class KnifeHitWall : MonoBehaviour
{
    private BoxCollider2D _hitbox;
    private Rigidbody2D _rigidbody;
    private DestroyProjectile _destroyProjectile;

    private void Start()
    {
        _hitbox = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _destroyProjectile = GetComponent<DestroyProjectile>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "Spike")
        {
            _hitbox.isTrigger = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.gravityScale = 0;
            _destroyProjectile.TouchesGround = true;
        }
        else if (collider.gameObject.tag == "LevelWall")
        {
            _destroyProjectile.DestroyNow = true;
        }
    }
}
