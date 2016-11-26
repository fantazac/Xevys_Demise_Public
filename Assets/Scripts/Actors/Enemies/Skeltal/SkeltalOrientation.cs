using UnityEngine;
using System.Collections;

public class SkeltalOrientation : ActorOrientation
{
    private void Start()
    {
        GetComponent<SkeltalBehaviour>().OnSkeltalMovementStart += Flip;
    }
}
