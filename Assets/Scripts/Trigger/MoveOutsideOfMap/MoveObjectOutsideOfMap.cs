using UnityEngine;
using System.Collections;

public class MoveObjectOutsideOfMap : MoveOutsideOfMap
{
    protected void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += MoveObjectOutside;
    }
}
