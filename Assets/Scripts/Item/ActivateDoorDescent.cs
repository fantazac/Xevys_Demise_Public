using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * C'est pour un trigger ? Pourquoi est-ce dans un dossier "Item" et non pas dans un dossier "Trigger" ? 
 */
public class ActivateDoorDescent : MonoBehaviour
{

    [SerializeField]
    private GameObject _door;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(_door != null)
        {
            _door.GetComponent<EnableDoor>().Descent = true;
        }
    }

}
