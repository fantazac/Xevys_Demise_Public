using UnityEngine;
using System.Collections;

public class RemoveObjectAfterCompletingMovement : MonoBehaviour
{

    private MoveObjectOnTrigger _moveObjectOnTrigger;

    private void Start()
    {
        _moveObjectOnTrigger = GetComponent<MoveObjectOnTrigger>();
        _moveObjectOnTrigger.OnFinishedMoving += RemoveObject;
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
    }
}
