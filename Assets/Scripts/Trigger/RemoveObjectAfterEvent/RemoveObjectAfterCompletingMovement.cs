using UnityEngine;
using System.Collections;

public class RemoveObjectAfterCompletingMovement : RemoveObjectAfterEvent
{
    protected void Start()
    {
        GetComponent<MoveObjectOnTrigger>().OnFinishedMoving += RemoveObject;
    }
}
