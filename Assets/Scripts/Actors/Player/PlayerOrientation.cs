using UnityEngine;
using System.Collections;

public class PlayerOrientation : ActorOrientation
{
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
