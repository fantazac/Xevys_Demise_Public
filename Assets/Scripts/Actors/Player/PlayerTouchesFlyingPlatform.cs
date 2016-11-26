using UnityEngine;
using System.Collections;

public class PlayerTouchesFlyingPlatform : MonoBehaviour
{
    private const float ENABLE_HITBOX_CD = 0.3f;

    private GameObject _flyingPlatform;

    private WaitForSeconds _enablePlatformDelay;
    private PlayerTouchesGround _playerTouchesGround;

    public bool OnFlyingPlatform { get; set; }

    private void Start()
    {
        _enablePlatformDelay = new WaitForSeconds(ENABLE_HITBOX_CD);
        _playerTouchesGround = GetComponent<PlayerTouchesGround>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().FlyingPlatform)
        {
            EnablePlatform();
            _flyingPlatform = collider.gameObject;
            OnFlyingPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().FlyingPlatform)
        {
            OnFlyingPlatform = false;
        }
    }

    private void EnablePlatform()
    {
        if(_flyingPlatform != null)
        {
            _flyingPlatform.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void DisablePlatform()
    {
        _flyingPlatform.GetComponent<BoxCollider2D>().enabled = false;
        OnFlyingPlatform = false;
        _playerTouchesGround.OnGround = false;
    }

    public void DropFromPlatform()
    {
        DisablePlatform();

        StartCoroutine(EnablePlatformWhenPlayerPassedThrough());
    }

    private IEnumerator EnablePlatformWhenPlayerPassedThrough()
    {
        yield return _enablePlatformDelay;
        
        EnablePlatform();
    }

    public bool HasFlyingPlatform()
    {
        return _flyingPlatform != null;
    }
}
