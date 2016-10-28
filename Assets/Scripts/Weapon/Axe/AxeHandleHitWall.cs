using UnityEngine;
using System.Collections;

public class AxeHandleHitWall : MonoBehaviour
{

    private const float BASE_AXE_DRAG = 5f;
    private bool _touchesGround = false;

    public bool TouchesGround { get { return _touchesGround; } set { _touchesGround = value; } }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "LevelWall" || collider.gameObject.tag == "Spike"
            || (collider.gameObject.tag == "FlyingPlatform" && transform.parent.GetComponent<Rigidbody2D>().velocity.y < 0))
        {
            GetComponent<PolygonCollider2D>().isTrigger = false;
            transform.parent.GetComponentInChildren<PolygonCollider2D>().isTrigger = false;
            transform.parent.GetComponent<Rigidbody2D>().drag = BASE_AXE_DRAG;
            _touchesGround = true;
            transform.parent.GetComponent<DestroyProjectile>().TouchesGround = true;
        }
    }
}
