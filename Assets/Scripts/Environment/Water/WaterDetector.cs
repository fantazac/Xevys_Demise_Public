//Source : https://github.com/tutsplus/unity-2d-water-effect/
//Ce code est basé sur cette source, mais je suis entrain de l'adapter pour nous. Il est sujet à changer.
using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour
{
    private const float WAVE_OFFSET = 0.5f;
    private const float SWIMMING_DAMPING_REDUCTION = 400f;
    private const float AXE_DAMPING_REDUCTION = 400f;
    private const float DEFAULT_DAMPING_REDUCTION = 60f;

    private ActorOrientation _orientation;

    private void Start()
    {
        _orientation = StaticObjects.GetPlayer().GetComponent<ActorOrientation>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "AxeHandle" || collider.gameObject.tag == "AxeBlade")
        {
            transform.parent.GetComponent<Water>().Splash(transform.position.x, collider.GetComponentInParent<Rigidbody2D>().velocity.y * collider.GetComponentInParent<Rigidbody2D>().mass / AXE_DAMPING_REDUCTION);
        }

        if (collider.GetComponent<Rigidbody2D>() != null)
        {
            if (collider.gameObject.tag == "Player" && collider.GetComponent<InventoryManager>().IronBootsActive && collider.GetComponent<PlayerWaterMovement>().enabled)
            {
                transform.parent.GetComponent<Water>().Splash(transform.position.x, collider.GetComponent<Rigidbody2D>().velocity.y * collider.GetComponent<Rigidbody2D>().mass / (DEFAULT_DAMPING_REDUCTION * 2));
            }
            else
            {
                transform.parent.GetComponent<Water>().Splash(transform.position.x, collider.GetComponent<Rigidbody2D>().velocity.y * collider.GetComponent<Rigidbody2D>().mass / DEFAULT_DAMPING_REDUCTION);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<Rigidbody2D>() != null && collider.tag == "Player")
        {
            //Creating the swimming wave ahead and behind the player
            transform.parent.GetComponent<Water>().Splash(transform.position.x + WAVE_OFFSET,
                                                         (_orientation.IsFacingRight ?
                                                         Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x) :
                                                         -Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x))
                                                         / SWIMMING_DAMPING_REDUCTION);
            transform.parent.GetComponent<Water>().Splash(transform.position.x - WAVE_OFFSET,
                                                         (_orientation.IsFacingRight ?
                                                         -Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x) :
                                                         Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x))
                                                         / SWIMMING_DAMPING_REDUCTION);
        }
    }

}
