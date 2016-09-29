using UnityEngine;
using System.Collections;

public class PlayerTouchesFlyingPlatform : MonoBehaviour
{
    private const int ENABLE_HITBOX_CD = 20;

    private GameObject _flyingPlatform;
    private bool _playerTouchesFlyingPlatform = false;
    private int _enableHitboxCount;

    public bool OnFlyingPlatform { get { return _playerTouchesFlyingPlatform; } set { _playerTouchesFlyingPlatform = value; } }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FlyingPlatform")
        {
            if (_flyingPlatform != null)
                EnablePlatformHitbox();

            _flyingPlatform = collider.gameObject;
            _playerTouchesFlyingPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FlyingPlatform")
            _playerTouchesFlyingPlatform = false;
    }

    public void DisablePlatformHitbox()
    {
        if(_flyingPlatform != null)
        {
            _flyingPlatform.GetComponent<BoxCollider2D>().enabled = false;
            _playerTouchesFlyingPlatform = false;
            GetComponent<PlayerTouchesGround>().OnGround = false;
            _enableHitboxCount = 0;
        }
            
    }

    private void EnablePlatformHitbox()
    {
        if (_flyingPlatform != null)
        {
            _flyingPlatform.GetComponent<BoxCollider2D>().enabled = true;
            _flyingPlatform = null;
            _enableHitboxCount = ENABLE_HITBOX_CD + 1;
        }
            
    }

    private void Start()
    {
        _enableHitboxCount = ENABLE_HITBOX_CD + 1;
    }

    private void Update()
    {
        //Debug.Log(_flyingPlatform != null);
        if (_flyingPlatform != null && !_playerTouchesFlyingPlatform && _enableHitboxCount == ENABLE_HITBOX_CD)
            EnablePlatformHitbox();
        else if (_enableHitboxCount < ENABLE_HITBOX_CD)
            _enableHitboxCount++;
    }
}
