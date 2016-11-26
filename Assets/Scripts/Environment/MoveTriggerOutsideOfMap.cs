using UnityEngine;
using System.Collections;

public class MoveTriggerOutsideOfMap: MonoBehaviour
{
    [SerializeField]
    private int _farAwayPosition = -1000;

    private void Start()
    {
        GetComponent<EyeAnimationOnDeath>().OnAnimationOver += MoveObjectOutside;
    }

    private void MoveObjectOutside()
    {
        transform.position = (Vector2.right + Vector2.up) * _farAwayPosition;
    }
}
