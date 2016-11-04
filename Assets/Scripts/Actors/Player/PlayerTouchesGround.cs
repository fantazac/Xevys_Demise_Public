using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * À fusionnner avec un composant qui représente l'état du joueur 
 * (Invincible, dans l'eau, sur le sol, vole, etc...)
 */
public class PlayerTouchesGround : MonoBehaviour
{

    private bool _playerTouchesGround = false;

    public bool OnGround { get { return _playerTouchesGround; } set { _playerTouchesGround = value; } }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "FlyingPlatform")
        {
            _playerTouchesGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "FlyingPlatform")
        {
            _playerTouchesGround = false;
        }
    }
}
