using UnityEngine;
using System.Collections;


/* BEN_CORRECTION
 * 
 * À fusionnner avec un composant qui représente l'état du joueur 
 * (Invincible, dans l'eau, sur le sol, vole, etc...)
 */
public class PlayerWaterInteraction : MonoBehaviour
{

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Water" && collider.transform.position.y < GetComponentInParent<Transform>().position.y)
        {
            GetComponentInParent<PlayerWaterMovement>().enabled = false;
            GetComponentInParent<PlayerGroundMovement>().enabled = true;

            GetComponentInParent<PlayerGroundMovement>().ChangeGravity();
        } 
    }
}
