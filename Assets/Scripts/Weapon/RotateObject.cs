using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * Pour la hache j'imagine, mais aussi pour autre chose potentiellement, et c'est pourquoi c'est appellé comme cela ?
 * 
 * À mettre dans un dossier de script communs.
 */
public class RotateObject : MonoBehaviour
{
    void Update()
    {
        if (GetComponentInChildren<PolygonCollider2D>().isTrigger && GetComponentInChildren<PolygonCollider2D>().isTrigger)
        {
            if (!GetComponent<SpriteRenderer>().flipY)
            {
                /* BEN_REVIEW
                 * 
                 * Constante magique : -5f.
                 */
                transform.Rotate(new Vector3(0, 0, -5f));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 5f));
            }
        }
    }
}
