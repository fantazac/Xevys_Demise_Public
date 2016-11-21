using UnityEngine;
using System.Collections;

public class SkeltalOrientation : ActorOrientation
{
    private SkeltalBehaviour _skeltalBehavior;

    private void Start()
    {
        _skeltalBehavior = GetComponent<SkeltalBehaviour>();
        _skeltalBehavior.OnSkeltalMovementStart += Flip;
    }
}
