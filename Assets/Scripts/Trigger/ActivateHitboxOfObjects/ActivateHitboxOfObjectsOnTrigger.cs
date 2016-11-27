using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnTrigger : ActivateHitboxOfObjects
{
    protected void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += Activate;
    }
}
