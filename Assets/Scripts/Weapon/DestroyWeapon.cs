using UnityEngine;
using System.Collections;

public class DestroyWeapon : MonoBehaviour
{

    private int _destroyCD;

    private bool _touchesGround;

    public bool TouchesGround { set { _touchesGround = value; } }

    void Start()
    {
        _touchesGround = false;
        _destroyCD = 0;
    }

    void Update()
    {
        if (_destroyCD < 50 && _touchesGround)
        {
            _destroyCD++;
        }
        else if (_destroyCD >= 50)
        {
            Destroy(gameObject);
        }
    }
}
