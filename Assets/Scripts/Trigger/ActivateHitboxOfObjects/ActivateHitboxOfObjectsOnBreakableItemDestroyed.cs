using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnBreakableItemDestroyed : ActivateHitboxOfObjects
{
    protected void Start()
    {
        GetComponent<Health>().OnDeath += Activate;
    }
}
