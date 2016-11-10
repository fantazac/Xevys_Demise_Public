using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour
{
    [SerializeField]
    private  float _delayAfterWallCollision = 0.833f;

    private bool _touchesGround;
    private bool _destroyNow;

    public bool TouchesGround { get { return _touchesGround; } set { _touchesGround = value; } }
    public bool DestroyNow { set { _destroyNow = value; } }

    public delegate void OnProjectileDestroyedHandler(GameObject projectile);
    public event OnProjectileDestroyedHandler OnProjectileDestroyed;

    void Start()
    {
        if (gameObject.tag != "Knife")
        {
            _touchesGround = GetComponentInChildren<AxeHandleHitWall>().TouchesGround;
        }

        _destroyNow = false;
    }

    void Update()
    {
        if (_touchesGround)
        {
            Destroy(gameObject, _delayAfterWallCollision);
            if (OnProjectileDestroyed != null)
            {
                OnProjectileDestroyed(gameObject);
            }           
            _touchesGround = false;
        }
        else if (_destroyNow)
        {
            OnProjectileDestroyed(gameObject);
            Destroy(gameObject);
        }
    }
}
