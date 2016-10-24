using UnityEngine;
using System.Collections;

public class FlipBoss : MonoBehaviour {

    [SerializeField]
    private bool _isFacingLeft;

    public bool IsFacingRight { get { return _isFacingLeft; } }
    public int Orientation { get { return (_isFacingLeft ? -1 : 1); } }

    public void CheckSpecificPointForFlip(Vector2 point)
    {
        if (point.x > transform.position.x)
        {
            if (_isFacingLeft)
            {
                Flip();
            }
        }
        else
        {
            if (!_isFacingLeft)
            {
                Flip();
            }
        }
    }

    public void CheckPlayerPosition()
    {
        /* BEN_REVIEW
         * 
         * À d'autres endroits, vous obtenez le player avec le tag "Player". Ici, vous l'obtenez avec le nom. 
         * Soyez consistants.
         */
        if (GameObject.Find("Character").transform.position.x > transform.position.x)
        {
            if (_isFacingLeft)
            {
                Flip();
            }
        }
        /* BEN_REVIEW
         * 
         * Évitez de faire plusieurs fois "Find" du même objet. Conservez des références quelque part, même si
         * c'est temporaire.
         */
        else if (GameObject.Find("Character").transform.position.x < transform.position.x)
        {
            if (!_isFacingLeft)
            {
                Flip();
            }
        }
    }

    /* BEN_REVIEW
     * 
     * Ceci est un TODO.
     * Mettre TODO devant le commentaire.
     */
    //In upcoming development, it would be wise to implement this method into a Component.
    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
