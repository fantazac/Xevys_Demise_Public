using UnityEngine;
using System.Collections;
using System.Linq;

public class AxeBladeHitWall : MonoBehaviour
{
    [SerializeField]
    private bool _canHitFlyingPlatform = false;

    [SerializeField]
    private string[] _canHitObjects;

    private RotateAxe _rotateAxe;
    private Rigidbody2D _rigidbody;
    private PolygonCollider2D _hitbox;
    private DestroyPlayerProjectile _destroyProjectile;

    private const float TIME_BEFORE_KINEMATIC = 0.05f;
    private const float FLYING_PLATFORM_OFFSET = 0.15f;

    private WaitForSeconds _delayDisableRigidbody;

    private void Start()
    {
        _delayDisableRigidbody = new WaitForSeconds(TIME_BEFORE_KINEMATIC);

        _rotateAxe = GetComponentInParent<RotateAxe>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _hitbox = GetComponent<PolygonCollider2D>();
        _destroyProjectile = GetComponentInParent<DestroyPlayerProjectile>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHitObject(collider))
        {
            StopAxe();
        }
    }

    private void StopAxe()
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


