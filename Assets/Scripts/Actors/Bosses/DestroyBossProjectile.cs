using UnityEngine;
using System.Collections;
using System.Linq;

/*
 * BEN_REVIEW
 * 
 * Pourrait facilement être généralisé. En fait, voici même le code modifié en commentaires plus bas.
 * 
 * Ensuite, à placer avec les scripts communs.
 * 
 * Réutilisation, réutilisation, réutilisation...
 */
public class DestroyBossProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}

/*
public class DestroyOnTriggerEnterWithTag : MonoBehaviour
{
    [SerializeField]
    private string[] _tags;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_tags.Contains(collider.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
*/
