using UnityEngine;

public abstract class ActorOrientation : MonoBehaviour
{
    [SerializeField]
    private bool _isFacingRight;

    public bool IsFacingRight { get { return _isFacingRight; } protected set { _isFacingRight = value; } }
    public int Orientation { get { return (_isFacingRight ? 1 : -1); } }

    protected void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
