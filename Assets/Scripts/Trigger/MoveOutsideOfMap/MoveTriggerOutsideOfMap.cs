using UnityEngine;
using System.Collections;

public class MoveTriggerOutsideOfMap : MoveOutsideOfMap
{
    protected void Start()
    {
        GetComponent<EyeAnimationOnDeath>().OnAnimationOver += MoveObjectOutside;
    }
}
