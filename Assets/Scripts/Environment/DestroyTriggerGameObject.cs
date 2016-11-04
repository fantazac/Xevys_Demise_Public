using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * Autrement dit, détruit l'objet lorsque cela reçoit des dégats d'un projectile ?
 */
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
