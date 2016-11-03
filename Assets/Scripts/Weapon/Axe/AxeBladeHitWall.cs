using UnityEngine;
using System.Collections;

public class AxeBladeHitWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "LevelWall" || collider.gameObject.tag == "Spike"
           || (collider.gameObject.tag == "FlyingPlatform" && transform.parent.GetComponent<Rigidbody2D>().velocity.y < 0))
        {
            GetComponentInParent<RotateAxe>().Rotate = false;
            GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponentInParent<Rigidbody2D>().gravityScale = 0;
            GetComponent<PolygonCollider2D>().isTrigger = false;
            GetComponentInParent<DestroyProjectile>().TouchesGround = true;
        }
    }
}


