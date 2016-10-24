using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * Remove trigger ? Donc, ça enlève le trigger ?
 * 
 * EDIT : Nope.
 */
public class RemoveTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "AxeBlade" || collider.gameObject.tag == "AxeHandle" || collider.gameObject.tag == "Knife")
        {
            Destroy(gameObject);
        }
    }

}
