using UnityEngine;
using System.Collections;

public class DestroyTriggerGameObject : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "AxeBlade" || collider.gameObject.tag == "AxeHandle" || collider.gameObject.tag == "Knife")
        {
            Destroy(gameObject);
        }
    }

}
