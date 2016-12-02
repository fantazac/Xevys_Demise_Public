using UnityEngine;
using System.Collections;

public class RemoveObjectAfterCompletingCinematicMovement : RemoveObjectAfterEvent
{
    protected void Start()
    {
        GetComponent<MoveObjectOnCinematicStarted>().OnFinishedMoving += RemoveObject;
    }
}
