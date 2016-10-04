using UnityEngine;
using System.Collections;

public class KnifeHitWall : MonoBehaviour
{

    private const float BASE_AXE_DRAG = 5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "FlyingPlatform")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<DestroyWeapon>().TouchesGround = true;
        }
    }
}
