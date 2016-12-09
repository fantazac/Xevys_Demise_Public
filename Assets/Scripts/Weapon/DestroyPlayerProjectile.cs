using UnityEngine;
using System.Collections;

public class DestroyPlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private  float _wallCollisionDuration = 0.833f;

    private bool _isBeingDestroyed = false;

    private WaitForSeconds _delayAfterWallCollision;

    public delegate void OnProjectileDestroyedHandler(GameObject projectile);
    public event OnProjectileDestroyedHandler OnProjectileDestroyed;

    private void Start()
    {
        _delayAfterWallCollision = new WaitForSeconds(_wallCollisionDuration);
    }

    public void TouchedWall()
    {
        if (!_isBeingDestroyed)
        {
            _isBeingDestroyed = true;
            StartCoroutine(DestroyProjectile(_delayAfterWallCollision));
        }
    }

    public void DestroyNow()
    {
        if (OnProjectileDestroyed != null)
        {
            OnProjectileDestroyed(gameObject);
        }

        Destroy(gameObject);
    }

    private IEnumerator DestroyProjectile(WaitForSeconds delay)
    {
        yield return delay;

        DestroyNow();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
