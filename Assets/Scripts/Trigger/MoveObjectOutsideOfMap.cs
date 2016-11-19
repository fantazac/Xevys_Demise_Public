using UnityEngine;
using System.Collections;

public class MoveObjectOutsideOfMap : MonoBehaviour
{

    private void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += MoveObjectOutside;
    }

    private void MoveObjectOutside()
    {
        transform.position = new Vector3(-1000, -1000, 0);
    }

}
