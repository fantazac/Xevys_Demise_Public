using UnityEngine;
using System.Collections;

public class DestroyWeapon : MonoBehaviour
{

    private const float DESTROY_DELAY_AFTER_HIT_WALL = 0.833f;
    private const float ONE_FRAME_DELAY = 0.022f;

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
    }

    void Update()
    {
        if (_touchesGround)
        {
            Destroy(gameObject, DESTROY_DELAY_AFTER_HIT_WALL);
        }
        else if (_destroyNow)
        {
            Destroy(gameObject, ONE_FRAME_DELAY);
        }
    }
}
