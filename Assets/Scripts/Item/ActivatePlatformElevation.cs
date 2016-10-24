using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * C'est pour un trigger ? Pourquoi est-ce dans un dossier "Item" et non pas dans un dossier "Trigger" ?
 * 
 * EDIT : N'y a-t-il pas un moyen de fusionner ça avec ActivateDoorDescent et ActivateHoverRetract ? 
 * Polymorphisme quelqu'un...
 */
public class ActivatePlatformElevation : MonoBehaviour
{

    [SerializeField]
    private GameObject _flyingPlatform;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_flyingPlatform != null)
        {
            /* BEN_REVIEW
             * 
             * Encore une fois, faire un composant à part pour le son.
             */
            GetComponent<AudioSource>().Play();
            _soundPlayed = true;

            gameObject.transform.position = new Vector3(-1000, -1000, 0);

            _flyingPlatform.GetComponent<ElevateFlyingPlatform>().Elevate = true;
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
