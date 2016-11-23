using UnityEngine;
using System.Collections;

public class AxeBladeHitWall : MonoBehaviour
{
    private RotateAxe _rotateAxe;
    private Rigidbody2D _rigidbody;
    private PolygonCollider2D _hitbox;
    private DestroyPlayerProjectile _destroyProjectile;
    private UnityTags _unityTags;

    private void Start()
    {
        _rotateAxe = GetComponentInParent<RotateAxe>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _hitbox = GetComponent<PolygonCollider2D>();
        _destroyProjectile = GetComponentInParent<DestroyPlayerProjectile>();
        _unityTags = StaticObjects.GetUnityTags();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == _unityTags.Wall || collider.gameObject.tag == _unityTags.LevelWall
            || collider.gameObject.tag == _unityTags.Spike
            || (collider.gameObject.tag == _unityTags.FlyingPlatform && _rigidbody.velocity.y < 0))
        {
            _rotateAxe.Rotate = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.gravityScale = 0;
            _hitbox.isTrigger = false;
            _destroyProjectile.TouchesGround = true;
        }
    }
}


