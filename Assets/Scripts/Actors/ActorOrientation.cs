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
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
