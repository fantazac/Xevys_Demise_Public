using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Rigidbody2D>() != null)
        {
            transform.parent.GetComponent<Water>().Splash(transform.position.x, collider.GetComponent<Rigidbody2D>().velocity.y * collider.GetComponent<Rigidbody2D>().mass / 40f);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<Rigidbody2D>() != null && collider.tag == "Player")
        {
            if (collider.GetComponent<PlayerMovement>().FacingRight)
            {
                transform.parent.GetComponent<Water>().Splash(transform.position.x + 0.6f, Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x) / 400f);
            }
            else
            {
                transform.parent.GetComponent<Water>().Splash(transform.position.x - 0.6f, Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x) / 400f);
            }
        }
    }

}
