using UnityEngine;

public class ActorOrientation : MonoBehaviour
{
    [SerializeField]
    private bool _isFacingRight;

    public bool IsFacingRight { get { return _isFacingRight; } protected set { _isFacingRight = value; } }
    public int Orientation { get { return (_isFacingRight ? 1 : -1); } }

    protected void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale += (_isFacingRight ? Vector3.right : Vector3.left) * 2 * transform.localScale.x;
    }

    public bool Flip(bool goesRight)
    {
        if (goesRight != IsFacingRight)
        {
            IsFacingRight = goesRight;
            return true;
        }
        return false;
    }
}
