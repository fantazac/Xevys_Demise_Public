using UnityEngine;
using System.Collections;

public class ActivateHitboxOfObjectsOnPlayerDeath : ActivateHitboxOfObjects
{
    protected void Start()
    {
        StaticObjects.GetPlayer().GetComponent<Health>().OnDeath += Activate;
    }
}
