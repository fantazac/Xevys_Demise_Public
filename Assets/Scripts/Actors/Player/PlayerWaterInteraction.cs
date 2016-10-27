using UnityEngine;
using System.Collections;

public class PlayerWaterInteraction : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider is BoxCollider2D)
        {
            GetComponentInParent<PlayerWaterMovement>().enabled = true;
            GetComponentInParent<PlayerGroundMovement>().enabled = false;
        }

        if (collider is CircleCollider2D)
        {
            GetComponentInParent<PlayerWaterMovement>().FeetTouchWater = true;
            GetComponentInParent<PlayerWaterMovement>().IsFloating = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider is BoxCollider2D && collider.transform.position.x > GetComponentInParent<Transform>().position.x)
        {
            GetComponentInParent<PlayerWaterMovement>().enabled = false;
            GetComponentInParent<PlayerGroundMovement>().enabled = true;
        }

        if (collider is CircleCollider2D)
        {
            GetComponentInParent<PlayerWaterMovement>().IsFloating = true;
        }  
    }
}
