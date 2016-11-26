using UnityEngine;
using System.Collections;

public class RemoveObjectAfterTrigger : RemoveObjectAfterEvent
{
    protected void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += RemoveObject;
    }
}
