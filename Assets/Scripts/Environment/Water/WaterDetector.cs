//Source : https://github.com/tutsplus/unity-2d-water-effect/
//Ce code est basé sur cette source, mais je suis entrain de l'adapter pour nous. Il est sujet à changer.
using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "AxeHandle" || collider.gameObject.tag == "AxeBlade")
        {
            transform.parent.GetComponent<Water>().Splash(transform.position.x, collider.GetComponentInParent<Rigidbody2D>().velocity.y * collider.GetComponentInParent<Rigidbody2D>().mass / 400f);
        }

        if (collider.GetComponent<Rigidbody2D>() != null)
        {
            transform.parent.GetComponent<Water>().Splash(transform.position.x, collider.GetComponent<Rigidbody2D>().velocity.y * collider.GetComponent<Rigidbody2D>().mass / 60f);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<Rigidbody2D>() != null && collider.tag == "Player")
        {
            if (collider.GetComponent<PlayerMovement>().FacingRight)
            {
                transform.parent.GetComponent<Water>().Splash(transform.position.x + 0.5f, Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x) / 400f);
                transform.parent.GetComponent<Water>().Splash(transform.position.x - 0.5f, -Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x) / 400f);
            }
            else
            {
                transform.parent.GetComponent<Water>().Splash(transform.position.x - 0.5f, Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x) / 400f);
                transform.parent.GetComponent<Water>().Splash(transform.position.x + 0.5f, -Mathf.Abs(collider.GetComponent<Rigidbody2D>().velocity.x) / 400f);
            }
        }
    }

}
