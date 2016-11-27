using UnityEngine;
using System.Collections;

public class DestroyPlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private  float _wallCollisionDuration = 0.833f;

    private WaitForSeconds _delayAfterWallCollision;

    public delegate void OnProjectileDestroyedHandler(GameObject projectile);
    public event OnProjectileDestroyedHandler OnProjectileDestroyed;

    private void Start()
    {
        _delayAfterWallCollision = new WaitForSeconds(_wallCollisionDuration);
    }

    public void TouchedWall()
    {
        StartCoroutine(DestroyProjectile(_delayAfterWallCollision));
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
}
