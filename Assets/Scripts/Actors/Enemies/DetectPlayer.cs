using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * PlayerDectector ?
 */
public class DetectPlayer : MonoBehaviour
{
    public bool DetectedPlayer { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /* BEN_REVIEW
         * 
         * Je veux que tous les TAGS soit centralisés dans une classe de constantes statiques
         * pour la prochaine correction.
         */
        if (collider.gameObject.tag == "Player")
        {
            DetectedPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DetectedPlayer = false;
        }
    }
}
