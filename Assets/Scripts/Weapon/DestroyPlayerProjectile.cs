using UnityEngine;
using System.Collections;

public class DestroyPlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private  float _wallCollisionDuration = 0.833f;

    private WaitForSeconds _delayAfterWallCollision;

    public bool TouchesGround { get; set; }
    public bool DestroyNow { get; set; }

    public delegate void OnProjectileDestroyedHandler(GameObject projectile);
    public event OnProjectileDestroyedHandler OnProjectileDestroyed;

    void Start()
    {
        _delayAfterWallCollision = new WaitForSeconds(_wallCollisionDuration);

        TouchesGround = false;
        DestroyNow = false;

        StartCoroutine(WaitForCollision());
    }

    private IEnumerator WaitForCollision()
    {
        while (!DestroyNow)
        {
            if (TouchesGround)
            {
                StartCoroutine(DestroyProjectile(_delayAfterWallCollision));
            }
            yield return null;
        }
        StartCoroutine(DestroyProjectile(null));
    }

    private IEnumerator DestroyProjectile(WaitForSeconds delay)
    {
        yield return delay;

        if (OnProjectileDestroyed != null)
        {
            OnProjectileDestroyed(gameObject);
        }

        Destroy(gameObject);
    }
}
