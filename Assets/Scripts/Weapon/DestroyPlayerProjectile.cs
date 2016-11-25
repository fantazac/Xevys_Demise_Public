using UnityEngine;
using System.Collections;

public class DestroyPlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private  float _wallCollisionDuration = 0.833f;

    private WaitForSeconds _delayAfterWallCollision;

    public delegate void OnProjectileDestroyedHandler(GameObject projectile);
    public event OnProjectileDestroyedHandler OnProjectileDestroyed;

    void Start()
    {
        _delayAfterWallCollision = new WaitForSeconds(_wallCollisionDuration);
    }

    public void TouchedWall()
    {
        Debug.Log(3);
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
        Debug.Log(2);
        yield return delay;

        DestroyNow();
    }
}
