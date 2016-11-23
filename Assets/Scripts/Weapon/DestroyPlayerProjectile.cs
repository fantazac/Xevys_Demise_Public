using UnityEngine;
using System.Collections;

public class DestroyPlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private  float _delayAfterWallCollision = 0.833f;

    public bool TouchesGround { get; set; }
    public bool DestroyNow { get; set; }

    public delegate void OnProjectileDestroyedHandler(GameObject projectile);
    public event OnProjectileDestroyedHandler OnProjectileDestroyed;

    void Start()
    {
        if (gameObject.tag == StaticObjects.GetUnityTags().Axe)
        {
            TouchesGround = GetComponentInChildren<AxeHandleHitWall>().TouchesGround;
        }

        DestroyNow = false;
    }

    void Update()
    {
        if (TouchesGround)
        {
            Destroy(gameObject, _delayAfterWallCollision);
            if (OnProjectileDestroyed != null)
            {
                OnProjectileDestroyed(gameObject);
            }
            TouchesGround = false;
        }
        else if (DestroyNow)
        {
            if (OnProjectileDestroyed != null)
            {
                OnProjectileDestroyed(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
