using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * C'est pour un trigger ? Pourquoi est-ce dans un dossier "Item" et non pas dans un dossier "Trigger" ? 
 * 
 * Aussi, je suis certain que l'on peut "généraliser" ce composant.
 * 
 * Enfin, ceci ne devrait pas s'occuper du son en même temps. Faites un autre composant.
 */
public class ActivateDoorRetract : MonoBehaviour
{

    [SerializeField]
    private GameObject _door;

    [SerializeField]
    private GameObject[] _wallsToActivate;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /* BEN_REVIEW
         * 
         * Le nom des TAG devrait être reçu en attribut (SerializeField).
         */
        if (_door != null && (collider.gameObject.tag == "AxeBlade" || collider.gameObject.tag == "AxeHandle"))
        {
            foreach (GameObject wall in _wallsToActivate)
            {
                wall.GetComponent<BoxCollider2D>().enabled = true;
            }

            GetComponent<AudioSource>().Play();
            _soundPlayed = true;
            /* BEN_REVIEW
             * 
             * Chiffres magiques.
             */
            gameObject.transform.position = new Vector3(-1000, -1000, 0);

            _door.GetComponent<RetractDoor>().Retract = true;
        }
    }

    void FixedUpdate()
    {
        if (_soundPlayed && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
