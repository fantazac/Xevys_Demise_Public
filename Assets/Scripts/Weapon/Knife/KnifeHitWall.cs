using UnityEngine;
using System.Collections;

public class KnifeHitWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "Spike")
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<DestroyProjectile>().TouchesGround = true;
        }
        else if (collider.gameObject.tag == "LevelWall")
        {
            GetComponent<DestroyProjectile>().DestroyNow = true;
        }
    }
}
