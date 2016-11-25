using UnityEngine;
using System.Collections;

public class DestroyBossDoorAfterMovement : MonoBehaviour
{

    private void Start()
    {
        GetComponent<MoveObjectOnTrigger>().OnFinishedMoving += DestroyDoor;
    }

    private void DestroyDoor()
    {
        Destroy(gameObject);
    }

}
