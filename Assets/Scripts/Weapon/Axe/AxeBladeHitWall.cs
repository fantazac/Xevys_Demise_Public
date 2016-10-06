using UnityEngine;
using System.Collections;

public class AxeBladeHitWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "LevelWall" || (collider.gameObject.tag == "FlyingPlatform" && transform.parent.GetComponent<Rigidbody2D>().velocity.y < 0))
        {
            transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.parent.GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<PolygonCollider2D>().isTrigger = false;
            transform.parent.GetComponent<DestroyWeapon>().TouchesGround = true;
        }
    }
}


