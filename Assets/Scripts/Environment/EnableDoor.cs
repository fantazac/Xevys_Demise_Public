using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * Cela sert à fermer des portes ?
 * 
 * Si c'est le cas, deux choses :
 *  1. Faire ouvrir ou fermer la porte devrait se faire par une méthode, pas par une propriété.
 *  2. Comment on ouvre une porte ?
 *  
 * À moins que "descent" veuille dire "Ouvrir la porte" ? Pas clair.
 * 
 * Aussi, c'est mal nommé. "Enable" ou "Close" ?
 */
public class EnableDoor : MonoBehaviour
{

    /* BEN_REVIEW
     * 
     * À paramêtrer dans l'éditeur, et non pas en constante.
     */
    private const float DESCENT_AMOUNT = 4f;
    private const float DESCENT_SPEED = 0.2f;

    private bool _descent = false;
    private float _descentCount = 0;

    public bool Descent { set { _descent = value; } }

    private void Update()
    {
        if (_descent)
        {
            if (_descentCount < DESCENT_AMOUNT)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - DESCENT_SPEED, transform.position.z);
                _descentCount += DESCENT_SPEED;
            }
            else
            {
                _descent = false;
            }
        }
    }

}
