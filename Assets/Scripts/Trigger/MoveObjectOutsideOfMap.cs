using UnityEngine;
using System.Collections;

public class MoveObjectOutsideOfMap : MonoBehaviour
{
    private ActivateTrigger _trigger;

    private void Start()
    {
        _trigger = GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += MoveObjectOutside;
    }

    private void MoveObjectOutside()
    {
        transform.position = new Vector3(-1000, -1000, 0);
    }

}
