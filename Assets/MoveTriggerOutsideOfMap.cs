using UnityEngine;
using System.Collections;

public class MoveTriggerOutsideOfMap: MonoBehaviour
{
    private EyeAnimationOnDeath _eyeAnimationOnDeath;

    private void Start()
    {
        _eyeAnimationOnDeath = GetComponent<EyeAnimationOnDeath>();
        _eyeAnimationOnDeath.OnAnimationOver += MoveObjectOutside;
    }

    private void MoveObjectOutside()
    {
        transform.position = new Vector3(-1000, -1000, 0);
    }

}
