using UnityEngine;
using System.Collections;

public class AxeHandleHitWall : MonoBehaviour
{

    private const float BASE_AXE_DRAG = 5f;

    public bool TouchesGround { get; set; }

    private void Start()
    {
        TouchesGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "LevelWall" || collider.gameObject.tag == "Spike"
            || (collider.gameObject.tag == "FlyingPlatform" && transform.parent.GetComponent<Rigidbody2D>().velocity.y < 0))
        {
            GetComponentInParent<RotateAxe>().Rotate = false;
            GetComponent<PolygonCollider2D>().isTrigger = false;
            transform.parent.GetComponentsInChildren<PolygonCollider2D>()[0].isTrigger = false;
            transform.parent.GetComponent<Rigidbody2D>().drag = BASE_AXE_DRAG;
            TouchesGround = true;
            transform.parent.GetComponent<DestroyProjectile>().TouchesGround = true;
        }
    }
}
