using UnityEngine;
using System.Collections;

public class PlayerTouchesFlyingPlatform : MonoBehaviour
{
    private const float ENABLE_HITBOX_CD = 0.22f;

    private GameObject _flyingPlatform;

    public bool OnFlyingPlatform { get; set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FlyingPlatform")
        {
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

    public void DropFromPlatform()
    {
        StartCoroutine("LetPlayerThroughPlatform");
    }

    private IEnumerator LetPlayerThroughPlatform()
    {
        _flyingPlatform.GetComponent<BoxCollider2D>().enabled = false;
        OnFlyingPlatform = false;
        GetComponent<PlayerTouchesGround>().OnGround = false;

        yield return new WaitForSeconds(ENABLE_HITBOX_CD);

        _flyingPlatform.GetComponent<BoxCollider2D>().enabled = true;
        _flyingPlatform = null;
    }
}
