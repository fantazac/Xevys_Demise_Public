using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * Ça représente le sens du boss ? Pourquoi cela ne parrait pas dans le nom ?
 * 
 * Aussi, ça ressemble beaucoup à "FlipPlayer". Possible fusion entre les deux ?
 * Usage d'héritage ?
 */
public class FlipBoss : MonoBehaviour {

    [SerializeField]
    private bool _isFacingLeft;
    GameObject player;

    public bool IsFacingLeft { get { return _isFacingLeft; } }
    public int Orientation { get { return (_isFacingLeft ? -1 : 1); } }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CheckSpecificPointForFlip(Vector2 point)
    {
        if (point.x > transform.position.x)
        {
            if (_isFacingLeft)
            {
                Flip();
            }
        }
        else if (point.x < transform.position.x)
        {
            if (!_isFacingLeft)
            {
                Flip();
            }
        }
    }

    /* BEN_CORRECTION
     * 
     * "Check" pour moi veut dire que cela retourne quelque chose...et c'est pas ça du tout que je vois.
     * 
     * TRÈS mal nommé. Pourquoi pas "FlipTowardPlayer" ? Mettez un verbe d'action dans les méthodes! S'il
     * n'y a pas de verbe d'action, c'est généralement mauvais signe.
     */
    public void CheckPlayerPosition()
    {
        if (player.transform.position.x > transform.position.x)
        {
            if (_isFacingLeft)
            {
                Flip();
            }
        }
        else if (player.transform.position.x < transform.position.x)
        {
            if (!_isFacingLeft)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
