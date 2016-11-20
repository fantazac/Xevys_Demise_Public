using UnityEngine;

public abstract class ActorOrientation : MonoBehaviour
{
    /*
     * BEN_REVIEW
     * 
     * Le rendre privé, mais laisser la propriété "protected set".
     */
    [SerializeField]
    protected bool _isFacingRight;

    public bool IsFacingRight { get { return _isFacingRight; } }
    public int Orientation { get { return (_isFacingRight ? 1 : -1); } }

    /*
     * BEN_REVIEW
     * 
     * Aucune raison que cette méthode existe à mon sens.
     */
    protected virtual void Start ()
    {
	
	}

    /*
     * BEN_REVIEW
     * 
     * Non virtuelle tandis qu'elle est surchargée ailleurs. Risque de problèmes.
     * 
     * EDIT : OH! Il y a un paramètre de plus dans "PlayerOrientation". Je crois que vous devriez repenser ces deux
     * classes. Pourquoi est-ce que le Player est géré différenmment des NPC ?
     */
    protected void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
