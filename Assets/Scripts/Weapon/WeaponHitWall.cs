using UnityEngine;
using System.Collections;

public class WeaponHitWall : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || (collider.gameObject.tag == "FlyingPlatform" && GetComponent<Rigidbody2D>().velocity.y < 0))
        {
           Destroy(gameObject);
        }
    }
}
