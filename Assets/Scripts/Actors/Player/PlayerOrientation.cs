using UnityEngine;
using System.Collections;

public class PlayerOrientation : ActorOrientation
{
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
