using UnityEngine;
using System.Collections;

public class PlayerTouchesGround : MonoBehaviour
{

    public bool OnGround { get; set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        SetOnGround(collider, true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        SetOnGround(collider, false);
    }

    private void SetOnGround(Collider2D collider, bool isOnGround)
    {
        if (ColliderIsGround(collider))
        {
            OnGround = isOnGround;
        }
    }

    private bool ColliderIsGround(Collider2D collider)
    {
        return collider.gameObject.tag == StaticObjects.GetObjectTags().Wall || 
            collider.gameObject.tag == StaticObjects.GetObjectTags().FlyingPlatform ||
            collider.gameObject.tag == StaticObjects.GetObjectTags().Spike;
    }
}
