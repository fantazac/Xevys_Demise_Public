using UnityEngine;
using System.Collections;

public class AxeBladeHitWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || (collider.gameObject.tag == "FlyingPlatform" && transform.parent.GetComponent<Rigidbody2D>().velocity.y < 0))
        {
            Debug.Log("Circle");
            Destroy(transform.parent.GetComponent<Rigidbody2D>());
            GetComponent<PolygonCollider2D>().isTrigger = false;
            transform.parent.GetComponent<DestroyWeapon>().TouchesGround = true;
        }
    }
}


