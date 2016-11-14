using UnityEngine;
using System.Collections;

public class PlayerTouchesGround : MonoBehaviour
{

    public bool OnGround { get; set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (ColliderIsGround(collider))
        {
            OnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (ColliderIsGround(collider))
        {
            OnGround = false;
        }
    }

    private bool ColliderIsGround(Collider2D collider)
    {
        return collider.gameObject.tag == "Wall" || collider.gameObject.tag == "FlyingPlatform";
    }
}
