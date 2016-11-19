using UnityEngine;
using System.Collections;

public class RemoveObjectAfterCompletingMovement : MonoBehaviour
{

    private void Start()
    {
        GetComponent<MoveObjectOnTrigger>().OnFinishedMoving += RemoveObject;
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
    }
}
