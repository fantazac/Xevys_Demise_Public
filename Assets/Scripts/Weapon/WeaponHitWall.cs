using UnityEngine;
using System.Collections;

public class WeaponHitWall : MonoBehaviour
{

    private const float BASE_AXE_DRAG = 5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || (collider.gameObject.tag == "FlyingPlatform" && GetComponent<Rigidbody2D>().velocity.y < 0))
        {
            if (gameObject.tag == "Knife")
            {
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<DestroyWeapon>().TouchesGround = true;
            }
        }
    }
}
