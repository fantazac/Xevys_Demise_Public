using UnityEngine;
using System.Collections;

public class FlipPlayer : MonoBehaviour
{

    [SerializeField]
    private bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; } }

    public bool Flip(bool goesRight)
    {
        if (goesRight != _isFacingRight)
        {
            _isFacingRight = goesRight;
            return true;
        }
        return false;
    }

}
