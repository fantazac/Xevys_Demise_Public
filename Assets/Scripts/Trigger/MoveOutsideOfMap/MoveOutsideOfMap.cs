using UnityEngine;
using System.Collections;

public class MoveOutsideOfMap : MonoBehaviour
{
    [SerializeField]
    protected int _farAwayPosition = -1000;

    protected virtual void MoveObjectOutside()
    {
        transform.position = (Vector2.right + Vector2.up) * _farAwayPosition;
    }
}
