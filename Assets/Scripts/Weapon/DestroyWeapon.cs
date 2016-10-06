using UnityEngine;
using System.Collections;

public class DestroyWeapon : MonoBehaviour
{

    private int _destroyCD;
    private int _destroyNowCD;

    private bool _touchesGround;
    private bool _destroyNow;

    public bool TouchesGround { get { return _touchesGround; } set { _touchesGround = value; } }
    public bool DestroyNow { set { _destroyNow = value; } }

    void Start()
    {
        if (gameObject.tag != "Knife")
        {
            _touchesGround = GetComponentInChildren<AxeHandleHitWall>().TouchesGround;
        }

        _destroyNow = false;
        _destroyCD = 0;
        _destroyNowCD = 0;
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
        if(_destroyNowCD < 1 && _destroyNow)
        {
            _destroyNowCD++;
        }
        else if(_destroyNowCD >= 1)
        {
            Destroy(gameObject);
        }
    }
}
