using UnityEngine;

public abstract class ActorOrientation : MonoBehaviour
{
    [SerializeField]
    protected bool _isFacingRight;

    public bool IsFacingRight { get { return _isFacingRight; } }
    public int Orientation { get { return (_isFacingRight ? 1 : -1); } }

    protected virtual void Start ()
    {
	
	}

    protected void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
