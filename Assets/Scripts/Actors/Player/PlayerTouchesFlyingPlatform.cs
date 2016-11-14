using UnityEngine;
using System.Collections;

public class PlayerTouchesFlyingPlatform : MonoBehaviour
{
    private const float ENABLE_HITBOX_CD = 0.3f;

    private GameObject _flyingPlatform;

    private WaitForSeconds _enablePlatformDelay;

    public bool OnFlyingPlatform { get; set; }

    private void Start()
    {
        _enablePlatformDelay = new WaitForSeconds(ENABLE_HITBOX_CD);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FlyingPlatform")
        {
            EnablePlatform();
            _flyingPlatform = collider.gameObject;
            OnFlyingPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FlyingPlatform")
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
        GetComponent<PlayerTouchesGround>().OnGround = false;
    }

    public void DropFromPlatform()
    {
        DisablePlatform();

        StartCoroutine("EnablePlatformWhenPlayerPassedThrough");
    }

    private IEnumerator EnablePlatformWhenPlayerPassedThrough()
    {
        yield return _enablePlatformDelay;

        EnablePlatform();
    }
}
