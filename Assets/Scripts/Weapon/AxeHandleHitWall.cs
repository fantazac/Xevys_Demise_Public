using UnityEngine;
using System.Collections;

public class AxeHandleHitWall : MonoBehaviour
{

    private const float BASE_AXE_DRAG = 5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || (collider.gameObject.tag == "FlyingPlatform" && transform.parent.GetComponent<Rigidbody2D>().velocity.y < 0))
        {
            GetComponent<PolygonCollider2D>().isTrigger = false;
            transform.parent.GetComponentInChildren<PolygonCollider2D>().isTrigger = false;
            transform.parent.GetComponent<Rigidbody2D>().drag = BASE_AXE_DRAG;
            transform.parent.GetComponent<DestroyWeapon>().TouchesGround = true;
        }
    }
}
